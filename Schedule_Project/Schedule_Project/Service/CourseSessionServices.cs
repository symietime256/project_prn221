using Schedule_Project.Models;

namespace Schedule_Project.Service
{
    public class CourseSessionServices
    {
        private readonly PRN221ProjectContext _context;
        public CourseSessionServices(PRN221ProjectContext context) {
            _context = context;
        }

        public List<CourseSession> GetSessions()
        {
            return _context.CourseSessions.ToList();
        }

        public void AddSessionsForCourse(CourseSession[] sessions) {
            _context.CourseSessions.AddRange(sessions);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
