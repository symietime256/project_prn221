using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedule_Project.Models;

namespace Schedule_Project.Pages.UniversityClasses
{
    public class CreateModel : PageModel
    {
        private readonly Schedule_Project.Models.PRN221ProjectContext _context;

        public CreateModel(Schedule_Project.Models.PRN221ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UniversityClass UniversityClass { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UniversityClasses == null || UniversityClass == null)
            {
                return Page();
            }

            _context.UniversityClasses.Add(UniversityClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
