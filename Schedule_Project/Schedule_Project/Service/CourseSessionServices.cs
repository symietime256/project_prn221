using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.SharingContent;

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
            public string GetTypeOfSlot(char slot)
        {
            switch (slot)
            {
                case 'A':
                    return Constant.MORNING;
                case 'B':
                    return Constant.AFTERNOON;
                case 'C':
                    return Constant.EVENING;
                default:
                    return Constant.INVALID_TYPE_OF_SLOT;
            }
        }
        private int GetSlots(int slot, string typeOfSlot)
        {
            int sessionSlot = slot;
            if (typeOfSlot.Equals(Constant.AFTERNOON))
            {
                sessionSlot = slot + 2;
            }
            else if (typeOfSlot.Equals(Constant.EVENING))
            {
                sessionSlot = slot + 4;
            }
            return sessionSlot;
        }



    }
}
