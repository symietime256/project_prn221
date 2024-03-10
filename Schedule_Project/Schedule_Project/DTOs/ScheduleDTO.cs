namespace Schedule_Project.DTOs
{
    public class ScheduleDTO
    {
        public ScheduleDTO() { }
        public string ClassId { get; set; }
        public string SubjectId { get; set; }

        public string Room { get; set; }

        public string Teacher { get; set; }

        public string SlotId { get; set; }
    }
}
