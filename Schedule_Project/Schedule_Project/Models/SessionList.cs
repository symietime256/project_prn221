using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class SessionList
    {
        public string ClassId { get; set; } = null!;
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string TeacherId { get; set; } = null!;

        public virtual Teacher Teacher { get; set; } = null!;
    }
}
