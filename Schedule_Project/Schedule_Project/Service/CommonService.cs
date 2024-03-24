using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.SharingContent;
using System.Text.RegularExpressions;

namespace Schedule_Project.Service
{
    public class CommonService
    {
        private readonly PRN221ProjectContext _context;

        private ScheduleServices ScheduleService;
        private CourseSessionServices CourseSessionServices;
        private TeacherServices teacherServices;
        private SubjectServices subjectServices;
        private UniversityClassesServices universityClassesServices;


        public bool checkDuplicateSlot(int slot1, int slot2)
        {
            if (slot1 == slot2)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public void DeleteAllOldSessions(Schedule schedule)
        {
            CourseSessionServices.DeleteOldCourseSession(schedule.Id);
        }

        public List<ScheduleDTO> TempScheduleDTOs;
        public CommonService(PRN221ProjectContext context, ScheduleServices scheduleService,
            CourseSessionServices courseSessionServices, TeacherServices teacherServices,
            SubjectServices subjectServices, UniversityClassesServices universityClassesServices) {
            _context = context;
            ScheduleService = scheduleService;
            CourseSessionServices = courseSessionServices;
            this.teacherServices = teacherServices;
            this.subjectServices = subjectServices;
            this.universityClassesServices = universityClassesServices;
            TempScheduleDTOs = new List<ScheduleDTO>();
        }

        public List<string> GetClassIdListFromCommonService()
        {
            return universityClassesServices.GetClassIdLists();
        }

        public ScheduleDTO GetScheduleDTO(Schedule s)
        {
            string typeOfSlot = GetTypeOfSlotCommon(s.TypeOfSlot);
            string slotId = typeOfSlot + s.Slot1 + s.Slot2;
            ScheduleDTO scheduleDTO = new(s.ClassId,
                s.SubjectId, s.Room, s.Teacher, slotId);
            return scheduleDTO;
        }

        public void AddSchedule(ScheduleDTO sdto) 
        {
            SlotInformationDTO sifDto = ScheduleService.GetSlotInformation(sdto);
            ScheduleService.CreateSchedule(sdto, sifDto);
            ImportEachSession(ScheduleService.ScheduleInCharge);
        }

        public Schedule GetEditScheduleDTO(ScheduleDTO sdto)
        {
            SlotInformationDTO sifDto = ScheduleService.GetSlotInformation(sdto);
            var schedule = ScheduleService.GetScheduleThroughDTO(sdto, sifDto);
            return schedule;
        }

        public void CleanTemporaryScheduleDTO()
        {
            TempScheduleDTOs.Clear();
        }

        public void GetAllData()
        {
            ScheduleService.GetSchedules();
            teacherServices.GetAllTeacherId();
            subjectServices.GetSubjectIdList();
            universityClassesServices.GetClassIdLists();
        }

        public int GetDayOfWeek(DateTime dateTime)
        {
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return (int)dateTime.DayOfWeek + 8;
            }
            return (int)dateTime.DayOfWeek + 1;
        }

        public bool ValidateCourse(ScheduleDTO scheduleDTO, int courseId = 0)
        {



            bool isValidCourse = false;
            char[] slotInformations = scheduleDTO.SlotId.ToCharArray();
            char typeSlotAbbeviation = slotInformations[0];
            string typeOfSlot = ScheduleService.GetTypeOfSlot(typeSlotAbbeviation);
            int slot1 = slotInformations[1] - '0';
            int slot2 = (int)slotInformations[2] - '0';
            
            if (checkDuplicateSlot(slot1, slot2)){
                return false;
            }

            foreach (var course in ScheduleService.SchedulesListInService)
            {
                if (course.Id == courseId) { continue; }

                if (!Regex.IsMatch(scheduleDTO.ClassId, Validate.CLASS_NAME)
                    || !Regex.IsMatch(scheduleDTO.Room, Validate.ROOM_NAME))
                {
                    isValidCourse = false;
                    break;
                }

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

        public string GetTypeOfSlotCommon(string typeOfSlot)
        {
            return typeOfSlot switch
            {
                "Morning" => "A",
                "Afternoon" => "B",
                "Evening" => "C",
                _ => "none",
            };
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

        public void ImportEachSession(Schedule schedule)
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
    }
}
