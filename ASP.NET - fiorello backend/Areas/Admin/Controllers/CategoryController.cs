using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET___fiorello_backend.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET___fiorello_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CategoryVM> categories = new();
            List<Category> dbCategories = await _context.Categories.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var category in dbCategories)
            {

                categories.Add(new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                }); ;

                //CategoryVM model = new()
                //{
                //    Id = category.Id,
                //    Name = category.Name,
                //};

                //categories.Add(model);
            }

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category newCategory = new()
            {
                Name = request.Name,
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Category category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            CategoryEditVM editCategory = new()
            {
                Name = category.Name,
            };

            return View(editCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }

            Category existedCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (existedCategory == null) return NotFound();

            if (existedCategory.Name.Trim() == request.Name.Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            Category category = new()
            {   
                Id = (int) id,
                Name = request.Name,
            };


            _context.Categories.Update(category);

            //existedCategory.Name = request.Name;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (dbCategory is null) return NotFound();

            CategoryDetailVM detailCategory = new()
            {
                Name = dbCategory.Name,
                CreatedDate = dbCategory.CreatedDate.ToString("MM.dd.yyyy")
            };

            return View(detailCategory);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    Category dbCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        //    _context.Categories.Remove(dbCategory);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            dbCategory.SoftDelete = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





    }
}
