using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            CourseSessions = new HashSet<CourseSession>();
        }

        public int Id { get; set; }
        public string ClassId { get; set; } = null!;
        public string SubjectId { get; set; } = null!;
        public string Room { get; set; } = null!;
        public string Teacher { get; set; } = null!;
        public string SlotId { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? Season { get; set; }
        public int? Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TypeOfSlot { get; set; } = null!;
        public int Slot1 { get; set; }
        public int Slot2 { get; set; }

        public virtual UniversityClass Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher TeacherNavigation { get; set; } = null!;
        public virtual ICollection<CourseSession> CourseSessions { get; set; }
    }
}
