using ASP.NET___fiorello_backend.Data;
using Microsoft.AspNetCore.Mvc;
using ASP.NET___fiorello_backend.ViewModels;
using Newtonsoft.Json;
using NuGet.ContentModel;
using Microsoft.EntityFrameworkCore;
using ASP.NET___fiorello_backend.Models;
using ASP.NET___fiorello_backend.Services.Interfaces;

namespace ASP.NET___fiorello_backend.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IBasketService _basket;
        private readonly IProductService _productService;
        public CartController(IHttpContextAccessor accessor, IBasketService basket, IProductService productService)
        {
            _accessor = accessor;
            _basket = basket;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketDetailVM> basketList = new();
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
                foreach (var data in basketDatas)
                {
                    var dbProduct = await _productService.GetByIdWithImagesAsync(data.Id);
                    if (dbProduct != null)
                    {
                        BasketDetailVM basketDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                            Image = dbProduct.Images.Where(m => m.IsMain).FirstOrDefault().Image,
                            Count = data.Count,
                            Price = dbProduct.Price,
                            TotalPrice = data.Count * dbProduct.Price
                        };
                        basketList.Add(basketDetail);
                    }
                   
                }
            }

            return View(basketList);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            List<BasketVM> basket = _basket.GetAllProducts();
            
            _basket.AddProduct(basket, product);

            int basketCount = basket.Sum(m => m.Count);

            return Ok(basketCount);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProductFromBasket(int? id)
        {
            return Ok(await _basket.DeleteProduct(id));
        }
    }
}
