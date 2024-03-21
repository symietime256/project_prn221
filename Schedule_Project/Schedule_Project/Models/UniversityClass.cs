using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class UniversityClass
    {
        public UniversityClass()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string ClassId { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
