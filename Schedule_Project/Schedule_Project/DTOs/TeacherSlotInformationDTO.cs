namespace Schedule_Project.DTOs
{
    public class TeacherSlotInformationDTO
    {

        public TeacherSlotInformationDTO() { }

        public TeacherSlotInformationDTO(int id, string classId, string subjectId, string room) {
            this.Id = id;
            ClassId = classId;
            SubjectId = subjectId;
            Room = room;
        }

        public int Id { get; set; }
        public string ClassId { get; set; }

        public string SubjectId { get; set; }

        public string Room {  get; set; }

        
    }
}
