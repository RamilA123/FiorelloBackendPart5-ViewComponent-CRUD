using ASP.NET___fiorello_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET___fiorello_backend.Models;
using ASP.NET___fiorello_backend.ViewModels;
using Newtonsoft.Json;
using NuGet.ContentModel;

namespace ASP.NET___fiorello_backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.Images).Take(4).ToListAsync();
            int count = await _context.Products.Where(m => !m.SoftDelete).CountAsync();
            ViewBag.count = count;
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Include(m =>m.Images).Skip(skip).Take(4).ToListAsync();
            return PartialView("_ProductsPartial", products);
        }
    }
}
