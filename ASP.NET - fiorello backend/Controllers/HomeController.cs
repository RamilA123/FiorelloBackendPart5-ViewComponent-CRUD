using Microsoft.AspNetCore.Mvc;
using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ASP.NET___fiorello_backend.ViewModels;
using ASP.NET___fiorello_backend.Services.Interfaces;

namespace ASP.NET___fiorello_backend.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _productService;
        public HomeController(AppDbContext context, IHttpContextAccessor contextAccessor, IProductService productService)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.Id).Take(3).ToListAsync();
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();
            IEnumerable<Product> products = await _productService.GetAllAsync();
            IEnumerable<Expert> experts = await _context.Experts.Include(m => m.Position).ToListAsync();


            HomeVM model = new()
            {
                Blogs = blogs,
                Categories = categories,
                Products = products,
                Experts = experts
            };

            return View(model);
        }
    }
}