using Microsoft.EntityFrameworkCore;
using Schedule_Project.DTOs;
using Schedule_Project.Models;
using Schedule_Project.SharingContent;
using System.Text.RegularExpressions;

namespace Schedule_Project.Service
{
    public class CourseSessionServices
    {
        private readonly PRN221ProjectContext _context;
        public CourseSessionServices(PRN221ProjectContext context) {
            _context = context;
        }



        public bool ValidateCourseSession(CourseSession courseSession)
        {

            bool isValid = true;
            var courseSessionList = _context.CourseSessions.Where(p => p.SessionDate.CompareTo(courseSession.SessionDate) == 0);
            foreach(var session in courseSessionList)
            {
                isValid = true;
                if (!Regex.IsMatch(courseSession.Room, Validate.ROOM_NAME))
                {
                    isValid = false;
                    break;
                }
                if (session.Slot == courseSession.Slot 
                    && (courseSession.Room == session.Room || courseSession.Teacher == session.Teacher))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }


        public List<CourseSession> GetSessionsByTeacher(string teacherName) {
            return _context.CourseSessions.Include(c => c.Course).Where(x => x.Teacher == teacherName).ToList();
        }
        public List<CourseSession> GetSessions()
        {
            return _context.CourseSessions.ToList();
        }

        public void DeleteOldCourseSession(int id)
        {
            var schedule = _context.Schedules.Include(p => p.CourseSessions).Single(x => x.Id == id);
            schedule.CourseSessions.Clear();
            SaveChanges();
        }

        public void DeleteCourseSessionById(int id)
        {
            int i = _context.CourseSessions.ToList().RemoveAll(x => x.Id == id);
            if (i > 0)
            {
                SaveChanges();
            } 
            
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
