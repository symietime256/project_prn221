using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.SharingContent;

namespace Schedule_Project.Service
{
    public class ScheduleServices
    {
        private readonly string semester = "Spring";
        private readonly int year = 2024;
        private readonly DateTime endDate = new DateTime(2024, 03, 23);

        private readonly PRN221ProjectContext _context;

        
        
        public List<Schedule> SchedulesListInService { get; set; }

        public Schedule ScheduleInCharge { get; set; }

        public SlotInformationDTO GetSlotInformation(ScheduleDTO scheduleDTO)
        {
            char[] slotInformations = scheduleDTO.SlotId.ToCharArray();
            char typeSlotAbbeviation = slotInformations[0];
            string typeOfSlot = GetTypeOfSlot(typeSlotAbbeviation);
            int slot1 = (int)slotInformations[1] - '0';
            int slot2 = (int)slotInformations[2] - '0';
            return new SlotInformationDTO(typeOfSlot, slot1, slot2);
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
        public void CreateSchedule(ScheduleDTO scheduleDTO, SlotInformationDTO slotInfo)
        {
            Schedule scheduleToSave = MapSchedule(scheduleDTO, slotInfo);
            SchedulesListInService.Add(scheduleToSave);
            _context.Schedules.Add(scheduleToSave);
            _context.SaveChanges();
            ScheduleInCharge = scheduleToSave;
        }

        private Schedule MapSchedule(ScheduleDTO scheduleDTO, SlotInformationDTO slotInfo)
        {
            Schedule schedule1 = new Schedule();
            schedule1.ClassId = scheduleDTO.ClassId;
            schedule1.Room = scheduleDTO.Room;
            schedule1.Teacher = scheduleDTO.Teacher;
            schedule1.SubjectId = scheduleDTO.SubjectId;
            schedule1.SlotId = scheduleDTO.SlotId;
            schedule1.Slot1 = slotInfo.Slot1;
            schedule1.Slot2 = slotInfo.Slot2;
            schedule1.TypeOfSlot = slotInfo.TypeOfSlot;
            schedule1.StartDate = GetStartDate(slotInfo.Slot1, slotInfo.Slot2);
            schedule1.EndDate = endDate;
            schedule1.Season = semester;
            schedule1.Year = year;
            schedule1.DateCreated = DateTime.Now;
            schedule1.HasSessionYet = false;
            return schedule1;
        }
        public DateTime GetStartDate(int slot1, int slot2)
        {
            int firstDay;
            firstDay = Math.Min(slot1, slot2);
            int year = DateTime.Now.Year;
            // Create a DateTime object for the first day of the year
            DateTime firstDayOfYear = new DateTime(year, 1, 1);
            DayOfWeek firstDayOfWeek = firstDayOfYear.DayOfWeek;

            int daysToAdd = (DayOfWeek.Monday + 7 - firstDayOfWeek) % 7;

            // Add the calculated number of days to the first day of the year to get the first Monday
            DateTime firstMondayOfYear = firstDayOfYear.AddDays(daysToAdd);
            DateTime firstDate = firstMondayOfYear.AddDays(firstDay - 2);
            return firstDate;
        }
        public ScheduleServices(PRN221ProjectContext context)
        {
            _context = context;
            SchedulesListInService = new List<Schedule>();
            ScheduleInCharge = new Schedule();
        }

        public List<Schedule> GetSchedules()
        {
            SchedulesListInService = _context.Schedules.ToList();
            return SchedulesListInService;
        }

        public void UpdateFlagAfterImportSessions(int id)
        {
            var course = _context.Schedules.Where(b => b.Id == id).FirstOrDefault();
            if (course != null)
            {
                // course.HasSessionYet.CurrentVa
                course.HasSessionYet = true;
                SaveChanges();
            }
        }
        

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
