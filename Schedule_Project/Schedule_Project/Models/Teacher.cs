using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Schedules = new HashSet<Schedule>();
            SessionLists = new HashSet<SessionList>();
        }

        public string TeacherId { get; set; } = null!;
        public string TeacherName { get; set; } = null!;
        public double Rating { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<SessionList> SessionLists { get; set; }
    }
}
