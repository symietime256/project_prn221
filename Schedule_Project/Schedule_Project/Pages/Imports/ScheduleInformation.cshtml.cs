using FileSignatures;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.Service;
using Schedule_Project.SharingContent;
using System;
using System.Globalization;
using System.Text.Json;
using static Schedule_Project.SharingContent.EnumSource;

namespace Schedule_Project.Pages.Imports
{
    public class ScheduleInformationModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private ScheduleServices ScheduleService;
        private CourseSessionServices CourseSessionServices;
        private TeacherServices teacherServices;
        private SubjectServices subjectServices;
        private UniversityClassesServices universityClassesServices;
        private CommonService commonService;
        private bool isValid;


        public Dictionary<int, Tuple<ScheduleDTO, bool>> CourseInformationStatus { get; set; }

        public HandleFileUpload HandleFileUploadModel { get; set; }
        public List<ScheduleDTO> SchedulesDTO { get; set; }

        public ScheduleInformationModel(IWebHostEnvironment environment, ScheduleServices scheduleService,
            CourseSessionServices courseSessionServices, TeacherServices teacherServices,
            SubjectServices subjectServices, UniversityClassesServices universityClassesServices,
            CommonService commonService)
        {
            _environment = environment;
            HandleFileUploadModel = new HandleFileUpload();
            ScheduleService = scheduleService;
            CourseSessionServices = courseSessionServices;
            this.teacherServices = teacherServices;
            this.subjectServices = subjectServices;
            this.universityClassesServices = universityClassesServices;
            this.commonService = commonService;
            CourseInformationStatus = new Dictionary<int, Tuple<ScheduleDTO, bool>>();
        }

        [BindProperty]
        public IFormFile FileUpload { get; set; }


        [TempData]
        public string Message { get; set; }

        private void GetAllData()
        {
            ScheduleService.GetSchedules();
            teacherServices.GetAllTeacherId();
            subjectServices.GetSubjectIdList();
            universityClassesServices.GetClassIdLists();
        }

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

            SchedulesDTO = HandleFileUploadModel.DeserializeListOfScheduleInformation(filePath, fileType);

            ValidateAndImportFile(SchedulesDTO);

            ImportToCourseSession();

            // Message = "File uploaded successfully!";
            string courseInfoStatusSerialize = JsonSerializer.Serialize(CourseInformationStatus);
            TempData["CourseResults"] = courseInfoStatusSerialize;
            return RedirectToPage("./ImportedCourseStatus");
        }

        private void ValidateAndImportFile(List<ScheduleDTO> scheduleDTOs)
        {
            GetAllData();
            int i = 1;
            foreach (ScheduleDTO scheduleDTO in scheduleDTOs)
            {
                isValid = true;
                if (!teacherServices.TeacherIdList.Contains(scheduleDTO.Teacher)
                    || !subjectServices.SubjectIdList.Contains(scheduleDTO.SubjectId))
                {
                    isValid = false;
                }
                if (!ValidateCourse(scheduleDTO))
                {
                    isValid = false;
                }
                if (isValid && !universityClassesServices.classLists.Contains(scheduleDTO.ClassId))
                {
                    char[] chars = scheduleDTO.ClassId.ToCharArray();
                    UniversityClass uc = new UniversityClass();
                    string majorType = chars[0] + "" + chars[1];
                    uc.ClassId = scheduleDTO.ClassId;
                    switch (majorType)
                    {
                        case "SE":
                            uc.Description = "Technology Class";
                            break;
                        case "MC":
                            uc.Description = "Media Class";
                            break;
                        default:
                            uc.Description = "Other";
                            break;
                    }
                    universityClassesServices.AddClass(uc);

                }
                if (isValid)
                {
                    SlotInformationDTO slotInformationDTO = ScheduleService.GetSlotInformation(scheduleDTO);
                    ScheduleService.CreateSchedule(scheduleDTO, slotInformationDTO);
                }
                var courseStatus = Tuple.Create(scheduleDTO, isValid);
                CourseInformationStatus.Add(i, courseStatus);
                i++;
            }
        }

        private bool ValidateCourse(ScheduleDTO scheduleDTO)
        {

            bool isValidCourse = false;
            char[] slotInformations = scheduleDTO.SlotId.ToCharArray();
            char typeSlotAbbeviation = slotInformations[0];
            string typeOfSlot = ScheduleService.GetTypeOfSlot(typeSlotAbbeviation);
            int slot1 = slotInformations[1] - '0';
            int slot2 = slotInformations[2] - '0';



            foreach (var course in ScheduleService.SchedulesListInService)
            {
                if (course.ClassId == scheduleDTO.ClassId && course.SubjectId == scheduleDTO.SubjectId)
                {
                    isValidCourse = false;
                    break;
                }
                if (course.Room == scheduleDTO.Room && course.TypeOfSlot == typeOfSlot &&
                    (course.Slot1 == slot1 || course.Slot2 == slot2))
                {
                    isValidCourse = false;
                    break;
                }

                if (course.Teacher == scheduleDTO.Teacher && course.TypeOfSlot == typeOfSlot
                    && (course.Slot1 == slot1 || course.Slot2 == slot2))
                {
                    isValidCourse = false;
                    break;
                }
                if (course.ClassId == scheduleDTO.ClassId && course.TypeOfSlot == typeOfSlot
                    && (course.Slot1 == slot1 || course.Slot2 == slot2)
                    && (course.Teacher != scheduleDTO.Teacher || course.Room != scheduleDTO.Room))
                {
                    isValidCourse = false;
                    break;
                }
                isValidCourse = true;
            }
            return isValidCourse;
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
            var courseInformationLists = ScheduleService.SchedulesListInService;
            foreach (var courseInformation in courseInformationLists)
            {
                if (!courseInformation.HasSessionYet)
                {
                    // ImportEachSession(courseInformation);
                    commonService.ImportEachSession(courseInformation);
                }

            }
        }




        private void ImportEachSession(Schedule schedule)
        {
            List<CourseSession> courseSessionsList = new List<CourseSession>();
            int numberOfSessions = subjectServices.GetSessionsBySubjectId(schedule.SubjectId);
            for (int i = 0; i <= numberOfSessions; i++)
            {
                DateTime d = schedule.StartDate;
                int dayOfWeek = (int)d.DayOfWeek + 1;
                int dayOfSlot;
                string typeOfSlot = schedule.TypeOfSlot;
                int week = i / 2 + 1;
                int slot;
                if (i % 2 == 0)
                {
                    dayOfSlot = schedule.Slot1;
                    slot = 1;
                }
                else
                {
                    dayOfSlot = schedule.Slot2;
                    slot = 2;
                }
                int gap = dayOfSlot - dayOfWeek;
                DateTime timeForSlot1 = d.AddDays(7 * (week - 1) + gap);
                int sessionSlot = GetSlots(slot, typeOfSlot);
                CourseSession cs = new(schedule.Id, i + 1, timeForSlot1, schedule.Teacher, schedule.Room, sessionSlot);
                courseSessionsList.Add(cs);

            }
            CourseSessionServices.AddSessionsForCourse(courseSessionsList.ToArray());
            ScheduleService.UpdateFlagAfterImportSessions(schedule.Id);
            // var schedule = ScheduleService.
            CourseSessionServices.SaveChanges();
        }

        public string GetTypeOfSlot(char slot)
        {
            switch (slot)
            {
                case 'A':
                    return Constant.MORNING;
                case 'B':
                    return Constant.AFTERNOON;
                case 'C':
                    return Constant.EVENING;
                default:
                    return Constant.INVALID_TYPE_OF_SLOT;
            }
        }
        private int GetSlots(int slot, string typeOfSlot)
        {
            int sessionSlot = slot;
            if (typeOfSlot.Equals(Constant.AFTERNOON))
            {
                sessionSlot = slot + 2;
            }
            else if (typeOfSlot.Equals(Constant.EVENING))
            {
                sessionSlot = slot + 4;
            }
            return sessionSlot;
        }



        public void OnGet()
        {

        }
    }
}
