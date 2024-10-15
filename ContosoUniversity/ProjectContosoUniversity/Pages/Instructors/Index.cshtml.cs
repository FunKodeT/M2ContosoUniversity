using ProjectContosoUniversity.Models;
//Add VM
using ProjectContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System;
//using Microsoft.AspNetCore.Mvc;
//using ProjectContosoUniversity.Data;

namespace ProjectContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;

        public IndexModel(ProjectContosoUniversity.Data.StudentContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        //public IList<Instructor> Instructor { get;set; } = default!;

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData();
            InstructorData.Instructors = await _context.Instructors.Include(i => i.OfficeAssignment).Include(i => i.Courses).ThenInclude(c => c.Department).OrderBy(i => i.LastName).ToListAsync();
            //Instructor = await _context.Instructors.ToListAsync();
            
            if(id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors.Where(i => i.ID == id.Value).Single();
                InstructorData.Courses = instructor.Courses;
            }

            if(courseID != null)
            {
                CourseID = courseID.Value;
                IEnumerable<Enrollment> Enrollments = await _context.Enrollments.Where(x => x.CourseID == CourseID).Include(i => i.Student).ToListAsync();
                InstructorData.Enrollments = Enrollments;
            }
        }
    }
}
