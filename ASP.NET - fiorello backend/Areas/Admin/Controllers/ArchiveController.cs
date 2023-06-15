using ASP.NET___fiorello_backend.Areas.Admin.ViewModels.Category;
using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET___fiorello_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly AppDbContext _context;

        public ArchiveController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            List<CategoryVM> categories = new();
            List<Category> dbCategories = await _context.Categories.IgnoreQueryFilters().Where(m => m.SoftDelete).ToListAsync();
            foreach (var category in dbCategories)
            {
                categories.Add(new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }

            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Extract(int id)
        {
            Category dbCategory = await _context.Categories.IgnoreQueryFilters().Where(m => m.SoftDelete).FirstOrDefaultAsync(m => m.Id == id);
            dbCategory.SoftDelete = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Categories));
        }

        


    }
}
