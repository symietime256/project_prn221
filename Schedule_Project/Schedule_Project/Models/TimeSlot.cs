using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class TimeSlot
    {
        public int SlotTimeId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
