using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string SubjectId { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? NumberOfSessions { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
