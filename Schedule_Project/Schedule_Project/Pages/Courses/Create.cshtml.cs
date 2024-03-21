using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedule_Project.Models;
using Schedule_Project.Service;

namespace Schedule_Project.Pages.Courses
{
    public class CreateModel : PageModel
    {

        private CommonService commonService;

        private UniversityClassesServices universityClassesService;

        [BindProperty]
        public string DescriptionForClass { get; set; } = "";

        [BindProperty]
        public string ErrorMessage { get; set; } = "";

        private readonly PRN221ProjectContext _context;

        public CreateModel(PRN221ProjectContext context, 
            CommonService commonService, UniversityClassesServices universityClassesServices)
        {
            this.commonService = commonService;
            _context = context;
            this.universityClassesService = universityClassesServices;
        }



        public IActionResult OnGet(string classexisted)
        {
            ViewData["classexisted"] = classexisted;
            SetViewData();
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;
        
        public void SetViewData()
        {
            ViewData["ClassId"] = new SelectList(_context.UniversityClasses, "ClassId", "ClassId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            ViewData["Teacher"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
        }

        public void OnPostCreateExisted(string classexisted)
        {
            ViewData["classexisted"] = classexisted;
            SetViewData();
            HandleCreateCourse();
        }

        public void OnPostCreateNew()
        {
            SetViewData();
            var classLists = commonService.GetClassIdListFromCommonService();
            if (!classLists.Contains(Schedule.ClassId))
            {
                UniversityClass uc = new()
                {
                    ClassId = Schedule.ClassId,
                    Description = DescriptionForClass
                };
                universityClassesService.AddClass(uc);
                universityClassesService.SaveChanges();
            }
            HandleCreateCourse();
        }

        public IActionResult HandleCreateCourse()
        {
            var scheduleDTO = commonService.GetScheduleDTO(Schedule);
            commonService.GetAllData();
            if (commonService.ValidateCourse(scheduleDTO))
            {
                commonService.AddSchedule(scheduleDTO);
                return Redirect("./Index");
            }
            else
            {
                ErrorMessage = "Failed because of conflicts";
                return Page();
            }
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Schedules == null || Schedule == null)
            {
                return Page();
            }

            _context.Schedules.Add(Schedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
