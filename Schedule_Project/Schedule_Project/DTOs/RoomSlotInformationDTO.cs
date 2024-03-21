namespace Schedule_Project.DTOs
{
    public class RoomSlotInformationDTO
    {
        public RoomSlotInformationDTO() { }

        public RoomSlotInformationDTO(string classID, string subjectId, string teacher)
        {
            ClassID = classID;
            SubjectId = subjectId;
            Teacher = teacher;
        }
        

        public string ClassID { get; set; }

        public string SubjectId { get; set; }

        public string Teacher { get; set; }

    }
}
