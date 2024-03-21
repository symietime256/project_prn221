using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.Models;

namespace Schedule_Project.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly Schedule_Project.Models.PRN221ProjectContext _context;

        public DetailsModel(Schedule_Project.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

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
    }
}
