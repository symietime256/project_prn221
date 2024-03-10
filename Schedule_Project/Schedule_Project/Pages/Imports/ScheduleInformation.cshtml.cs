using FileSignatures;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Schedule_Project.Models;
using static Schedule_Project.SharingContent.EnumSource;

namespace Schedule_Project.Pages.Imports
{
    public class ScheduleInformationModel : PageModel
    {
        private readonly PRN221ProjectContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileFormatInspector _fileFormatInspector;

        public ScheduleInformationModel(PRN221ProjectContext context, IWebHostEnvironment environment, IFileFormatInspector fileFormatInspector)
        {
            _context = context;
            _environment = environment;
            _fileFormatInspector = fileFormatInspector;
        }

        [BindProperty]
        public IFormFile FileUpload { get; set; }
        

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (FileUpload == null || FileUpload.Length == 0)
            {
                Message = "Please select a file.";
                return Page();
            }

            using (var stream = new MemoryStream())                                                                                                                                                                    
            {
                await FileUpload.CopyToAsync(stream);
                var fileType = GetFileType(stream.ToArray());
                switch (fileType)
                {
                    case FileType.JSON:
                        Message = "JSON";
                        break;
                    case FileType.XML:
                        Message = "XML";
                        break;

                    default:
                        Message = "Error File Type";
                        break;
                }
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, FileUpload.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await FileUpload.CopyToAsync(stream);
            }

            // Message = "File uploaded successfully!";
            return RedirectToPage("/Index");
        }

        private FileType GetFileType(byte[] bytes)
        {
            if (bytes.Take(2).SequenceEqual(new byte[] { 0x3c, 0x3f }))
            {
                return FileType.XML;
            }
            if (bytes.Take(1).SequenceEqual(new byte[] { 0x7b }))
            {
                return FileType.JSON;
            }
            else
            {
                return FileType.Unknown;
            }
        }

        public void OnGet()
        {
        }
    }
}
