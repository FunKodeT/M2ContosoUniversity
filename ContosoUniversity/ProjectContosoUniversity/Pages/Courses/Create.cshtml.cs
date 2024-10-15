using ProjectContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using ProjectContosoUniversity.Data;

namespace ProjectContosoUniversity.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public CreateModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_context);
            //ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCourse = new Course();
            // Prefix for form value.
            if(await TryUpdateModelAsync<Course>(emptyCourse, "course", s => s.CourseID, s => s.DepartmentID, s => s.Title, s => s.Credits))
            {
                _context.Courses.Add(emptyCourse);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);
            return Page();
        }
    }
}
