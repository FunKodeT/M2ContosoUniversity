using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectContosoUniversity.Data;
using ProjectContosoUniversity.Models;

namespace ProjectContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public DetailsModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            
            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
