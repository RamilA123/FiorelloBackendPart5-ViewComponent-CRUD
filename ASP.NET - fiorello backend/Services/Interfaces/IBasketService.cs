using ASP.NET___fiorello_backend.Models;
using ASP.NET___fiorello_backend.Responses;
using ASP.NET___fiorello_backend.ViewModels;
namespace ASP.NET___fiorello_backend.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetAllProducts();
        void AddProduct(List<BasketVM> basket, Product product);
        Task<BasketDeleteResponse> DeleteProduct(int? id);
        int GetProductsByCount();

    }
}
