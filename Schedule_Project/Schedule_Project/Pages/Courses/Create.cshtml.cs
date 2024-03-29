﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedule_Project.Models;
using Schedule_Project.Service;
using Schedule_Project.SharingContent;

namespace Schedule_Project.Pages.Courses
{
    public class CreateModel : PageModel
    {

        private CommonService commonService;

        private UniversityClassesServices universityClassesService;

        [BindProperty]
        public string DescriptionForClass { get; set; } = "";

        [BindProperty]
        public string ErrorMessage { get; set; } = "";

        private readonly PRN221ProjectContext _context;

        public CreateModel(PRN221ProjectContext context, 
            CommonService commonService, UniversityClassesServices universityClassesServices)
        {
            this.commonService = commonService;
            _context = context;
            this.universityClassesService = universityClassesServices;
        }



        public IActionResult OnGet(string classexisted)
        {
            ViewData["classexisted"] = classexisted;
            SetViewData();
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;
        
        public void SetViewData()
        {
            ViewData["ClassId"] = new SelectList(_context.UniversityClasses, "ClassId", "ClassId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            ViewData["Teacher"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
        }

        public void OnPostCreateExisted(string classexisted)
        {
            ViewData["classexisted"] = classexisted;
            SetViewData();
            HandleCreateCourse();
        }

        public void OnPostCreateNew()
        {
            SetViewData();
            var classLists = commonService.GetClassIdListFromCommonService();
            bool legitNewClass = true;
            if (!classLists.Contains(Schedule.ClassId) && Regex.IsMatch(Schedule.ClassId, Validate.CLASS_NAME))
            {
                UniversityClass uc = new()
                {
                    ClassId = Schedule.ClassId,
                    Description = DescriptionForClass
                };
                universityClassesService.AddClass(uc);
                universityClassesService.SaveChanges();
            }else if (!Regex.IsMatch(Schedule.ClassId, Validate.CLASS_NAME))
            {
                legitNewClass = false;
            }
            HandleCreateCourse(legitNewClass);
        }

        public IActionResult HandleCreateCourse(bool legitNewClass = true)
        {
            var scheduleDTO = commonService.GetScheduleDTO(Schedule);
            commonService.GetAllData();
            if (!legitNewClass)
            {
                ErrorMessage = "Failed because of conflicts";
                return Page();
            }
            if (commonService.ValidateCourse(scheduleDTO))
            {
                commonService.AddSchedule(scheduleDTO);
                ErrorMessage = "Create course successfully";
                return Redirect("./Index");
            }
            else
            {
                ErrorMessage = "Failed because of conflicts";
                return Page();
            }
            
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //public async Task<IActionResult> OnPostAsync()
        //{
        //  if (!ModelState.IsValid || _context.Schedules == null || Schedule == null)
        //    {
        //        return Page();
        //    }

        //    _context.Schedules.Add(Schedule);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
