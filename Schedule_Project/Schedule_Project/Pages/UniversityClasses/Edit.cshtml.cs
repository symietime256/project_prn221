using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.Models;

namespace Schedule_Project.Pages.UniversityClasses
{
    public class EditModel : PageModel
    {
        private readonly Schedule_Project.Models.PRN221ProjectContext _context;

        public EditModel(Schedule_Project.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UniversityClass UniversityClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.UniversityClasses == null)
            {
                return NotFound();
            }

            var universityclass =  await _context.UniversityClasses.FirstOrDefaultAsync(m => m.ClassId == id);
            if (universityclass == null)
            {
                return NotFound();
            }
            UniversityClass = universityclass;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UniversityClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityClassExists(UniversityClass.ClassId))
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

        private bool UniversityClassExists(string id)
        {
          return (_context.UniversityClasses?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}
