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
    public class IndexModel : PageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public IndexModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get;set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses.Include(c => c.Department).AsNoTracking().ToListAsync();
        }
    }
}
