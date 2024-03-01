using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.Models;

namespace Schedule_Project.Pages.UniversityClasses
{
    public class DetailsModel : PageModel
    {
        private readonly Schedule_Project.Models.PRN221ProjectContext _context;

        public DetailsModel(Schedule_Project.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

      public UniversityClass UniversityClass { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.UniversityClasses == null)
            {
                return NotFound();
            }

            var universityclass = await _context.UniversityClasses.FirstOrDefaultAsync(m => m.ClassId == id);
            if (universityclass == null)
            {
                return NotFound();
            }
            else 
            {
                UniversityClass = universityclass;
            }
            return Page();
        }
    }
}
