using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.Models;
using Schedule_Project.Service;

namespace Schedule_Project.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly PRN221ProjectContext _context;

        private readonly CourseSessionServices sessionServices;

        public DeleteModel(PRN221ProjectContext context, CourseSessionServices css)
        {
            sessionServices = css;
            _context = context;
        }

        [BindProperty]
      public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FirstOrDefaultAsync(m => m.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }
            else 
            {
                Schedule = schedule;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            int id1 = (int)id;
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }
            var schedule = await _context.Schedules.Include(b => b.CourseSessions).FirstOrDefaultAsync(p => p.Id == id1);

            if (schedule != null)
            {
                
                Schedule = schedule;
                _context.Remove(Schedule);

                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
