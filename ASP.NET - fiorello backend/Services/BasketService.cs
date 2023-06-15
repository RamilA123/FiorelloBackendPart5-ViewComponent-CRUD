using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Models;
using ASP.NET___fiorello_backend.Responses;
using ASP.NET___fiorello_backend.Services.Interfaces;
using ASP.NET___fiorello_backend.ViewModels;
using Newtonsoft.Json;
using NuGet.ContentModel;

namespace ASP.NET___fiorello_backend.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public BasketService(IHttpContextAccessor accessor, IProductService productService)
        {
            _accessor = accessor;
            _productService = productService;
        }

        public void AddProduct(List<BasketVM> basket, Product product)
        {
            var existedProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existedProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existedProduct.Count++;
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public async Task<BasketDeleteResponse> DeleteProduct(int? id)
        {
            List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            var data = basketDatas.FirstOrDefault(m => m.Id == id);
            basketDatas.Remove(data);
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            int count = basketDatas.Sum(m => m.Count);
            decimal total = 0;
            foreach (var basketData in basketDatas) 
            { 
                Product dbProduct = await _productService.GetByIdAsync(basketData.Id);
                total += (dbProduct.Price * basketData.Count);
            }
            return new BasketDeleteResponse { Count = count, Total = total };
        }


        public List<BasketVM> GetAllProducts()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        public int GetProductsByCount()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket.Sum(m => m.Count);
        }
    }
}
