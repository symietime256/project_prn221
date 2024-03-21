namespace Schedule_Project.DTOs
{
    public class SlotInformationDTO
    {

        public SlotInformationDTO(string typeOfSlot, int slot1, int slot2)
        {
            TypeOfSlot = typeOfSlot;
            Slot1 = slot1;
            Slot2 = slot2;
        }

        public string TypeOfSlot { get; set; }
        public int Slot1 { get; set; }
        public int Slot2 { get; set; }
    }
}
