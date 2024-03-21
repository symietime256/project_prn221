using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schedule_Project.DTOs;
using Schedule_Project.Models;

namespace Schedule_Project.Pages.ShowSchedule
{
    public class DisplayScheduleByDaysModel : PageModel
    {
        public HashSet<string> RoomLists { get; set; }
        [BindProperty(SupportsGet =true)]
        public DateTime SelectedDate { get; set; }

        public ScheduleCellsDTO ScheduleCells { get; set; }

        private readonly PRN221ProjectContext _context;

        public DisplayScheduleByDaysModel(PRN221ProjectContext context)
        {
            _context = context;
            if (ScheduleCells == null)
            {
                ScheduleCells = new ScheduleCellsDTO();
                SelectedDate = DateTime.Now;
            }
            RoomLists = new HashSet<string>();
        }

        public void GetScheduleData()
        {
            var roomSlotInformation = _context.CourseSessions.Include(courseSession => courseSession.Course).Where(d => d.SessionDate == SelectedDate).ToList();
            foreach (var roomSlot in roomSlotInformation)
            {
                RoomSlotInformationDTO rsi = new(roomSlot.Course.ClassId, roomSlot.Course.SubjectId, roomSlot.Teacher);
                Tuple<string, int> tuple = new Tuple<string, int>(roomSlot.Room, roomSlot.Slot);
                ScheduleCells.scheduleCells.Add(tuple, rsi);
                RoomLists.Add(roomSlot.Room);
            }
            gay();

        }
        public void gay()
        {
            Console.WriteLine("haha");
            Console.WriteLine("mmb");
        }
        public void OnGet()
        {
            GetScheduleData();
        }
    }
}
