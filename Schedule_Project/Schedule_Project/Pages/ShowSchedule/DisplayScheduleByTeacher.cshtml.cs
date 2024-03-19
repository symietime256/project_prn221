using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Schedule_Project.Service;

namespace Schedule_Project.Pages.ShowSchedule
{
    public class DisplayScheduleByTeacherModel : PageModel
    {

        public CommonService CommonService { get; set; }

        public DisplayScheduleByTeacherModel(CommonService commonService) {
            CommonService = commonService;  
        }
        public void OnGet()
        {
        }
    }
}
