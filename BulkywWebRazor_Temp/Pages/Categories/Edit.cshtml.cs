using BulkywWebRazor_Temp.Data;
using BulkywWebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkywWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
      
        private readonly ApplicationDbContext _db;
     
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int id)
        {
            Category = _db.Category.Find(id);
            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoryInDb = _db.Category.Find(Category.Id);
            if (categoryInDb == null)
            {
                return NotFound();
            }

            // Update properties
            _db.Entry(categoryInDb).CurrentValues.SetValues(Category);


            _db.SaveChanges();

            return RedirectToPage("Index");
        }

    }
}
