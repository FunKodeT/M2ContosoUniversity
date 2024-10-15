using ProjectContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using ProjectContosoUniversity.Data;

namespace ProjectContosoUniversity.Pages.Courses
{
    public class EditModel : DepartmentNamePageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public EditModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseID == id);
            //var course =  await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (Course == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(_context, Course.DepartmentID);
            //Course = Course;
            //ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var courseToUpdate = await _context.Courses.FindAsync(id);
            if(courseToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Course>(courseToUpdate, "course", c => c.Credits, c => c.DepartmentID, c => c.Title))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, courseToUpdate.DepartmentID);
            return Page();
        }

        //private bool CourseExists(int id)
        //{
        //    return _context.Courses.Any(e => e.CourseID == id);
        //}
    }
}
