using ProjectContosoUniversity.Data;
using ProjectContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProjectContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL {  get; set; }

        public void PopulateDepartmentsDropDownList(StudentContext _context, object selectedDepartment = null)
        {
            //Sort by Name.
            var departmentsQuery = from d in _context.Departments orderby d.Name select d;

            DepartmentNameSL = new SelectList(departmentsQuery.AsNoTracking(), nameof(Department.DepartmentID), nameof(Department.Name), selectedDepartment);
        }
    }
}
