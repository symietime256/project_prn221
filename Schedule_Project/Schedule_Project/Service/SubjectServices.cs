﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Schedule_Project.Models;

namespace Schedule_Project.Service
{
    public class SubjectServices
    {
        private readonly PRN221ProjectContext _context;
        public SubjectServices(PRN221ProjectContext context) {
            _context = context;
            SubjectIdList = new List<string>();
        }

        public List<string> SubjectIdList;

        public List<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public List<string> GetSubjectIdList()
        {
            var subjectLists = GetSubjects();
            SubjectIdList = new List<string>();
            foreach (var subject in subjectLists)
            {
                SubjectIdList.Add(subject.SubjectId);
            }
            return SubjectIdList;
        }
    }
}
