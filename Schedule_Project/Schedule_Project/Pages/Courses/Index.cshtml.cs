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
    public class IndexModel : PageModel
    {
        private readonly Schedule_Project.Models.PRN221ProjectContext _context;

        public IndexModel(Schedule_Project.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        public IList<Schedule> Schedule { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Schedules != null)
            {
                Schedule = await _context.Schedules
                .Include(s => s.Class)
                .Include(s => s.Subject)
                .Include(s => s.TeacherNavigation).ToListAsync();
            }
        }
    }
}
