using BulkywWebRazor_Temp.Data;
using BulkywWebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkywWebRazor_Temp.Pages.Categories
{
    public class indexModel : PageModel
    {

        //in razor pages we have asp-page for routing to page  instead of asp-controller in.cs file
        // we have .cshtml and .cshtml.cs file in razor pages
        private readonly ApplicationDbContext _db;
        [BindProperty]//it binds .cshtml and .cshtml.cs
        public List<Category> CategoryList { get; set; }
        public indexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Category.ToList();

        }

    }
}
