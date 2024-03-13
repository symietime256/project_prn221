using Schedule_Project.Models;
using System.Security.Policy;

namespace Schedule_Project.Service
{
    public class UniversityClassesServices
    {
        private readonly PRN221ProjectContext _context;

        public List<string> classLists;
        public UniversityClassesServices(PRN221ProjectContext context)
        {
            _context = context;
            classLists = new List<string>();
        }

        public void AddClass(UniversityClass universityClass)
        {
            _context.UniversityClasses.Add(universityClass);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<UniversityClass> GetAllUniversityClasses()
        {
            return _context.UniversityClasses.ToList();
        }

        public List<string> GetClassIdLists()
        {
            var classes = GetAllUniversityClasses();
            classLists = new List<string>();
            foreach (var clazz in classes)
            {
                classLists.Add(clazz.ClassId);
            }
            return classLists;

        }


    }
}
