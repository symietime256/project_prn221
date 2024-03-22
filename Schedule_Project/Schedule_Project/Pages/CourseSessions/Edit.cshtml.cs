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

namespace Schedule_Project.Pages.CourseSessions
{
    public class EditModel : PageModel
    {
        private readonly PRN221ProjectContext _context;

        private CourseSessionServices css;

        public string ErrorMessage { get; set; } = "";

        public EditModel(PRN221ProjectContext context, CourseSessionServices css)
        {
            this.css = css;
            _context = context;
        }

        [BindProperty]
        public CourseSession CourseSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CourseSessions == null)
            {
                return NotFound();
            }

            var coursesession =  await _context.CourseSessions.Include(c => c.Course).FirstOrDefaultAsync(m => m.Id == id);
            if (coursesession == null)
            {
                return NotFound();
            }
            CourseSession = coursesession;
            ViewData["CourseId"] = new SelectList(_context.Schedules, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ErrorMessage = " ";
            //var course = await _context.Schedules.Include(s => s.Subject).Include(t => t.TeacherNavigation).Include(c => c.Class).Include(cs => cs.CourseSessions).Where(c => c.Id == CourseSession.CourseId).FirstOrDefaultAsync();
            //if (course == null)
            //{
            //    ErrorMessage = "This courses for the slot is not found";
            //    return Page();
            //}
            //course.CourseSessions.Add(CourseSession);
            ViewData["CourseId"] = new SelectList(_context.Schedules, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            if (!css.ValidateCourseSession(CourseSession))
            {
                ErrorMessage = "This edited slot is conflict with other slots";
                return Page();
            }

            try
            {
                var coursesession = await _context.CourseSessions.Include(c => c.Course).FirstOrDefaultAsync(m => m.Id == CourseSession.Id);
                if (coursesession != null)
                {
                    coursesession.SessionDate = CourseSession.SessionDate;
                    coursesession.Teacher = CourseSession.Teacher;
                    coursesession.Room = CourseSession.Room;
                    coursesession.Slot = CourseSession.Slot;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseSessionExists(CourseSession.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage($"../ShowSchedule/DisplayScheduleByDays");
        }

        private bool CourseSessionExists(int id)
        {
          return (_context.CourseSessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
