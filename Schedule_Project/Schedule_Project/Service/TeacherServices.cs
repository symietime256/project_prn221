using Schedule_Project.Models;

namespace Schedule_Project.Service
{
    public class TeacherServices
    {
        private readonly PRN221ProjectContext _context;
        public TeacherServices(PRN221ProjectContext context) {
            _context = context;
            TeacherIdList = new List<string>();
        }

        public List<string> TeacherIdList;

        public List<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList();
        }

        public List<string> GetAllTeacherId()
        {
            var teacherLists = GetTeachers();
            TeacherIdList = new List<string>();
            foreach (var teacher in teacherLists)
            {
                TeacherIdList.Add(teacher.TeacherId);
            }
            return TeacherIdList;
        }
    }
}
