﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectContosoUniversity.Data;
using ProjectContosoUniversity.Models;

namespace ProjectContosoUniversity.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public IndexModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Department = await _context.Departments
                .Include(d => d.Administrator).ToListAsync();
        }
    }
}
