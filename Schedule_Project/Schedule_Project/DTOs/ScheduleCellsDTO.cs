namespace Schedule_Project.DTOs
{
    public class ScheduleCellsDTO
    {
        public Dictionary<Tuple<string, int>, RoomSlotInformationDTO> scheduleCells;
        public ScheduleCellsDTO()
        {
            scheduleCells = new Dictionary<Tuple<string, int>, RoomSlotInformationDTO>();
        }
    }
}
