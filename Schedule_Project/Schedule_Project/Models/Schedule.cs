﻿using System;
using System.Collections.Generic;

namespace Schedule_Project.Models
{
    public partial class Schedule
    {
        public string Id { get; set; } = null!;
        public string ClassId { get; set; } = null!;
        public string SubjectId { get; set; } = null!;
        public string Room { get; set; } = null!;
        public string Teacher { get; set; } = null!;
        public string SlotId { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string Season { get; set; } = null!;
        public int Year { get; set; }

        public virtual UniversityClass Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher TeacherNavigation { get; set; } = null!;
    }
}
