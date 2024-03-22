namespace Schedule_Project.DTOs
{
    public class RoomSlotInformationDTO
    {
        public RoomSlotInformationDTO() { }

        public RoomSlotInformationDTO(int id, string classID, string subjectId, string teacher)
        {
            Id = id;
            ClassID = classID;
            SubjectId = subjectId;
            Teacher = teacher;
        }
        
        public int Id { get; set; }

        public string ClassID { get; set; }

        public string SubjectId { get; set; }

        public string Teacher { get; set; }

    }
}
