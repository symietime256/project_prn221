using System.ComponentModel.DataAnnotations;

namespace Schedule_Project.DTOs
{
    public class ScheduleDTO
    {
        public ScheduleDTO() { }

        public ScheduleDTO(string classId, string subjectId, string room, string teacher, string SlotId) {
            this.SlotId = SlotId;
            SubjectId = subjectId;
            Room = room;
            Teacher = teacher;
            ClassId = classId;
        }
        public string ClassId { get; set; }
        public string SubjectId { get; set; }

        public string Room { get; set; }

        public string Teacher { get; set; }

        [StringLength(3, ErrorMessage="Phải dài 1 đến 3 ký tự")]
        public string SlotId { get; set; }
    }

    public class ScheduleListDTO
    {
        public ScheduleListDTO() { }

        public List<ScheduleDTO> ListOfScheduleInformation { get; set; }
    }
}
