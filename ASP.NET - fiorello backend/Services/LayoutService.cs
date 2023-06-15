using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Services.Interfaces;
using ASP.NET___fiorello_backend.Models;
using Microsoft.VisualBasic;
using ASP.NET___fiorello_backend.ViewModels;
using Newtonsoft.Json;

namespace ASP.NET___fiorello_backend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basket;

        public LayoutService(AppDbContext context, IBasketService basket)
        {
            _context = context;
            _basket = basket;
        }

        public LayoutVM GetSettingDatas()
        {
            int count = _basket.GetProductsByCount();
            Dictionary<string, string> datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);   
            return new LayoutVM { BasketCount = count, SettingDatas = datas };

            //LayoutVM model = new()
            //{
            //   BasketCount = count,
            //   SettingDatas = datas
               
            //};

            //return model;

            //Setting headerData = _context.Settings.FirstOrDefault(m => m.Key == "HeaderLogo");
            //Dictionary<string, string> footerDatas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            //return model;
        }

       

    }
}
