using ASP.NET___fiorello_backend.ViewModels;
using ASP.NET___fiorello_backend.Data;
using ASP.NET___fiorello_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET___fiorello_backend.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();
            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();

            SliderVM model = new()
            {
                SliderInfo = sliderInfo,
                Sliders = sliders
            };

            return await Task.FromResult(View(model));
        }

    }
}
