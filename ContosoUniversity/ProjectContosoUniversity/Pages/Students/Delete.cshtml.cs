using ProjectContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ProjectContosoUniversity.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectContosoUniversity.Data.StudentContext _context;
        private readonly ILogger<DeleteModel> _logger;
        
        public DeleteModel(ProjectContosoUniversity.Data.StudentContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Student Student { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            if(Student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }
            return Page();

            //var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);

            //if (student == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    Student = student;
            //}
            //return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            //if (student != null)
            //{
            //    Student = student;
            //    _context.Students.Remove(Student);
            //    await _context.SaveChangesAsync();
            //}
            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToAction("./Delete", new {id, saveChangesError = true});
            }
            //return RedirectToPage("./Index");
        }
    }
}
