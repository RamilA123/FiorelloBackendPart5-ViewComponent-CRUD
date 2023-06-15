using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Models;
using ASP.NET___fiorello_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET___fiorello_backend.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Include(m => m.Images).ToListAsync();
        public async Task<Product> GetByIdAsync(int? id) => await _context.Products.FindAsync(id);
        public async Task<Product> GetByIdWithImagesAsync(int? id) => await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
       
    }
}
