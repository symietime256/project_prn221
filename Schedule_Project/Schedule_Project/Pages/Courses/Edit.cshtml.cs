using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.Models;
using Schedule_Project.Service;

namespace Schedule_Project.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly Schedule_Project.Models.PRN221ProjectContext _context;

        private CommonService commonService;

        [BindProperty]
        public string ErrorMessage { get; set; } = "";

        public EditModel(Schedule_Project.Models.PRN221ProjectContext context, CommonService commonService)
        {
            _context = context;
            this.commonService = commonService;
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule =  await _context.Schedules.FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }
            Schedule = schedule;
           ViewData["ClassId"] = new SelectList(_context.UniversityClasses, "ClassId", "ClassId");
           ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
           ViewData["Teacher"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.

        public IActionResult HandleEditCourse()
        {
            var scheduleDTO = commonService.GetScheduleDTO(Schedule);
            commonService.GetAllData();
            if (commonService.ValidateCourse(scheduleDTO))
            {
                Schedule = commonService.GetEditScheduleDTO(scheduleDTO);
                return Redirect("./Index");
            }
            else
            {
                ErrorMessage = "Failed because of conflicts";
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            HandleEditCourse();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(Schedule.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduleExists(int id)
        {
          return (_context.Schedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
