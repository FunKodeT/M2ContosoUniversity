using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectContosoUniversity.Data;
using ProjectContosoUniversity.Models;

namespace ProjectContosoUniversity.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public DetailsModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses.AsNoTracking().Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseID == id);
            //var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (Course == null)
            {
                return NotFound();
            }
            //else
            //{
            //    Course = course;
            //}
            return Page();
        }
    }
}
