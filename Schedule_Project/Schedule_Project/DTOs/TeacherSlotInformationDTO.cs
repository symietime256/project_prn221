namespace Schedule_Project.DTOs
{
    public class TeacherSlotInformationDTO
    {

        public TeacherSlotInformationDTO() { }

        public TeacherSlotInformationDTO(string classId, string subjectId, string room) {
            ClassId = classId;
            SubjectId = subjectId;
            Room = room;
        }

        public string ClassId { get; set; }

        public string SubjectId { get; set; }

        public string Room {  get; set; }

        
    }
}
