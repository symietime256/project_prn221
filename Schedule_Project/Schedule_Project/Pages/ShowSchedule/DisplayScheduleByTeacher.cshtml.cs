using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.Service;
using System.Net.WebSockets;
using System.Security.Policy;

namespace Schedule_Project.Pages.ShowSchedule
{
    public class DisplayScheduleByTeacherModel : PageModel
    {

        private TeacherServices teacherServices;
        private CourseSessionServices courseSessionServices;
        public CommonService CommonService { get; set; }

        [BindProperty]
        public string CurrentTeacher { get; set; } = "";

        [BindProperty]
        public List<string> TeacherLists { get; set; }

        public List<string> ListOfDayOfWeek { get; set; }

        private DateTime startDate;

        public Dictionary<string, Tuple<DateTime, DateTime>> DateDictionary { get; set; }

        public Dictionary<Tuple<int, int>, TeacherSlotInformationDTO> ScheduleCells { get; set; }

        [BindProperty]
        public string Period { get; set; } = "";

        private void AddDateSpring(bool onGet = false)
        {
            DateTime d1 = DateTime.Now;
            if (d1.Month >= 1 && d1.Month < 5)
            {
                startDate = new(2024, 1, 1);

            }

            for (int i = 1; i <= 15; i++)
            {
                DateTime beginWeek = startDate.AddDays(7 * (i - 1));
                DateTime endWeek = startDate.AddDays(6 + 7 * (i - 1));
                var value = Tuple.Create(beginWeek, endWeek);
                string begin = $"{beginWeek.Day}/{beginWeek.Month}";
                string end = $"{endWeek.Day}/{endWeek.Month}";
                string gay = $"{begin} To {end}";
                if (onGet && DateTime.Compare(beginWeek, DateTime.Now) < 0 &&
                        DateTime.Compare(DateTime.Now, endWeek) < 0)
                {
                    Period = gay;
                }
                DateDictionary.Add(gay, value);
            }
        }

        public void GetFirstTeacher()
        {
            var gay = teacherServices.GetTeachers().FirstOrDefault();
            CurrentTeacher = gay.TeacherId;
        }

        

        public DisplayScheduleByTeacherModel(CommonService commonService,
            TeacherServices teacherServices, CourseSessionServices courseSessionServices)
        {
            ScheduleCells = new Dictionary<Tuple<int, int>, TeacherSlotInformationDTO>();
            CommonService = commonService;
            this.teacherServices = teacherServices;
            this.courseSessionServices = courseSessionServices;
            TeacherLists = new List<string>();
            DateDictionary = new Dictionary<string, Tuple<DateTime, DateTime>>();
            ListOfDayOfWeek = new List<string>
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
        }

        public List<CourseSession> GetCourseSessionsFromTeacher()
        {
            var period = DateDictionary[Period];
            List<CourseSession> list = courseSessionServices.GetSessionsByTeacher(CurrentTeacher).AsEnumerable()
                .Where(p => p.SessionDate.CompareTo(period.Item1) >= 0 && p.SessionDate.CompareTo(period.Item2) <= 0).ToList();
            return list;

        }

        public void SetUpForScheduleCells(List<CourseSession> courseSessionsByTeacher)
        {
            foreach (var c in courseSessionsByTeacher)
            {
                TeacherSlotInformationDTO tc = new TeacherSlotInformationDTO(c.Id ,c.Course.ClassId, c.Course.SubjectId, c.Room);
                int dayOfWeek = CommonService.GetDayOfWeek(c.SessionDate);
                int slot = c.Slot;
                Tuple<int, int> dayAndSlotTuple = Tuple.Create(dayOfWeek, slot);
                ScheduleCells.Add(dayAndSlotTuple, tc);
            }
        }

        public void OnPost()
        {
            SetViewData();
        }

        public void SetViewData(bool onGet = false)
        {
            TeacherLists = teacherServices.GetAllTeacherId();
            AddDateSpring(onGet);
            var courseSessionsByTeacher = GetCourseSessionsFromTeacher();
            SetUpForScheduleCells(courseSessionsByTeacher);

        }

        

        public void OnGet()
        {
            GetFirstTeacher();
            SetViewData(true);
            // binding 
            
            

        }
    }
}
