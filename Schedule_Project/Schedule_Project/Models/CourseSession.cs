using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class CourseSession
    {

        public CourseSession() { }

        public CourseSession(int courseId, int sessionId, DateTime sessionDate, string teacher, string room, int slot) {
            CourseId = courseId;
            SessionId = sessionId;
            SessionDate = sessionDate;
            Teacher = teacher;
            Room = room;
            Slot = slot;
        }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public string Teacher { get; set; } = null!;
        public string Room { get; set; } = null!;
        public int Slot { get; set; }

        public virtual Schedule Course { get; set; } = null!;
    }
}
