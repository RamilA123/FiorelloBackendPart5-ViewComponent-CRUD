using ASP.NET___fiorello_backend.Models;
namespace ASP.NET___fiorello_backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetByIdWithImagesAsync(int? id);
    }
}
