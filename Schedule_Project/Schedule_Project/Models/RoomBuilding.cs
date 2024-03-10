using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class RoomBuilding
    {
        public string BuildingType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int NumberOfRooms { get; set; }
        public int? RoomsInEachFloor { get; set; }
    }
}
