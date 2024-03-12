using FileSignatures;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.SharingContent;
using System;
using System.Globalization;
using static Schedule_Project.SharingContent.EnumSource;

namespace Schedule_Project.Pages.Imports
{
    public class ScheduleInformationModel : PageModel
    {
        private readonly PRN221ProjectContext _context;
        private readonly IWebHostEnvironment _environment;

        public HandleFileUpload HandleFileUploadModel { get; set; }
        public List<ScheduleDTO> Schedules { get; set; }

        public ScheduleInformationModel(PRN221ProjectContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            HandleFileUploadModel = new HandleFileUpload();
            Schedules = new List<ScheduleDTO>();
        }

        [BindProperty]
        public IFormFile FileUpload { get; set; }
        

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (FileUpload == null || FileUpload.Length == 0)
            {
                Message = "Please select a file.";
                return Page();
            }

            FileType fileType;
            using (var stream = new MemoryStream())                                                                                                                                                                    
            {
                await FileUpload.CopyToAsync(stream);
                fileType = GetFileType(stream.ToArray());
                switch (fileType)
                {
                    case FileType.JSON:
                        Message = "JSON";
                        break;
                    case FileType.XML:
                        Message = "XML";
                        break;

                    default:
                        Message = "Error File Type";
                        break;
                }
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, FileUpload.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await FileUpload.CopyToAsync(stream);
            }

            Schedules = HandleFileUploadModel.DeserializeListOfScheduleInformation(filePath, fileType);
            ImportToCourseSession();
            // ImportToCourseSession();

            // Message = "File uploaded successfully!";
            return RedirectToPage("/Index");
        }

        private FileType GetFileType(byte[] bytes)
        {
            byte[] sampleWhitespace = new byte[] { 0x20, 0x09, 0x0A, 0x0D, 0x0B, 0x0C };
            int i;
            for (i = 0; i < bytes.Length; i++)
            {
                if (!sampleWhitespace.Contains(bytes[i]))
                {
                    break;
                }
            }
            if (bytes.Skip(i).Take(2).SequenceEqual(new byte[] { 0x3c, 0x3f }))
            {
                return FileType.XML;
            }
            if (bytes.Skip(i).Take(1).SequenceEqual(new byte[] { 0x7b }) || bytes.Skip(i).Take(1).SequenceEqual(new byte[] { 0x5b }))
            {
                return FileType.JSON;
            }
            else
            {
                return FileType.Unknown;
            }
        }

        public void ImportToCourseSession()
        {
            var courseInformationLists = _context.Schedules.ToList();
            foreach (var courseInformation in courseInformationLists)
            {
                if (courseInformation.Id == 2)
                {
                    ImportEachSession(courseInformation);
                }
                
            }
        } 

        private void ImportEachSession(Schedule schedule)
        {

            int year = DateTime.Now.Year;
            // Create a DateTime object for the first day of the year
            DateTime firstDayOfYear = new DateTime(year, 1, 1);
            DayOfWeek firstDayOfWeek = firstDayOfYear.DayOfWeek;

            int daysToAdd = (DayOfWeek.Monday + 7 - firstDayOfWeek) % 7;

            // Add the calculated number of days to the first day of the year to get the first Monday
            DateTime firstMondayOfYear = firstDayOfYear.AddDays(daysToAdd);
            List<CourseSession> courseSessionsList = new List<CourseSession>();
            for (int i=0; i<=19; i++)
            {
                DateTime d = schedule.StartDate;
                int dayOfWeek = (int)d.DayOfWeek + 1;
                int dayOfSlot;
                string typeOfSlot = schedule.TypeOfSlot;
                int week = i / 2 + 1;
                if (i % 2  == 0)
                {
                    dayOfSlot = schedule.Slot1;
                    int gap = dayOfSlot - dayOfWeek;
                    int slot = 1;
                    DateTime timeForSlot1 = d.AddDays(7*(week-1) + gap);
                    int sessionSlot = GetSlots(slot, typeOfSlot);
                    CourseSession cs = new(schedule.Id, i+1, timeForSlot1, schedule.Teacher, schedule.Room, sessionSlot);
                    courseSessionsList.Add(cs);

                }
               else
                {
                    dayOfSlot = schedule.Slot2;
                    int gap = dayOfSlot - dayOfWeek;
                    int slot = 2;
                    DateTime timeForSlot2 = d.AddDays(7 * (week-1) + gap);
                    int sessionSlot = GetSlots(slot, typeOfSlot);
                    CourseSession cs = new(schedule.Id, i+1, timeForSlot2, schedule.Teacher, schedule.Room, sessionSlot);
                    courseSessionsList.Add(cs);
                }

            }
                _context.CourseSessions.AddRange(courseSessionsList.ToArray());
                _context.SaveChanges();
        }


        private int GetSlots(int slot, string typeOfSlot)
        {
            int sessionSlot = slot;
            if (typeOfSlot.Equals(Constant.AFTERNOON))
            {
                sessionSlot = slot + 2;
            } else if (typeOfSlot.Equals(Constant.EVENING))
            {
                sessionSlot = slot + 4;
            }
            return sessionSlot;
        }

        private void ImportSessionDate(DayOfWeek dayOfWeek)
        {

        }

        public void OnGet()
        {
            
        }
    }
}
