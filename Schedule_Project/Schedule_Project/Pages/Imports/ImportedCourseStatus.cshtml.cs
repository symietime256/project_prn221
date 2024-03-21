using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Schedule_Project.DTOs;
using System.Text.Json;

namespace Schedule_Project.Pages.Imports
{
    public class ImportedCourseStatusModel : PageModel
    {

        public Dictionary<int, Tuple<ScheduleDTO, bool>> CourseInformationStatus { get; set; }

        public ImportedCourseStatusModel() {
            CourseInformationStatus = new Dictionary<int, Tuple<ScheduleDTO, bool>>();
        
        }
        public void OnGet()
        {
            var serializedCourseResult = TempData["CourseResults"].ToString();
            Dictionary<int, Tuple<ScheduleDTO, bool>>? courseResults = JsonSerializer.Deserialize<Dictionary<int, Tuple<ScheduleDTO, bool>>>(serializedCourseResult);
            if (courseResults != null)
            {
                CourseInformationStatus = courseResults;
            }

        }

        
    }
}
