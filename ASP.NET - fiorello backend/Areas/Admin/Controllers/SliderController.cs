using Microsoft.AspNetCore.Mvc;
using ASP.NET___fiorello_backend.Models;
using ASP.NET___fiorello_backend.Data;
using Microsoft.EntityFrameworkCore;
using ASP.NET___fiorello_backend.Areas.Admin.ViewModels;
using ASP.NET___fiorello_backend.Areas.Admin.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.NET___fiorello_backend.Helpers;

namespace ASP.NET___fiorello_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliderList = new List<SliderVM>();
            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();
            foreach (Slider slider in sliders)
            {
                SliderVM sliderVM = new()
                {
                    Id = slider.Id,
                    Image = slider.Image,
                    Status = slider.Status,
                };

                sliderList.Add(sliderVM);
            }


            return View(sliderList);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider is null) return NotFound();
            SliderDetailVM sliderDetail = new()
            {
                Image = slider.Image,
                CreateDate = slider.CreatedDate.ToString("MMMM dd, yyyy"),
                Status = slider.Status,
            };

            return View(sliderDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!FileExtension.CheckFileType(request.Image,"image/"))
            {
                ModelState.AddModelError("Image", "Please select only image file");
                return View();
            }

            if (FileExtension.CheckFileSize(request.Image))
            {
                ModelState.AddModelError("Image", "Image size must be 70 KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;

            await FileExtension.SaveFileAsync(_env.WebRootPath, "img", fileName, request.Image);
         
            Slider newSlider = new()
            {
                Image = fileName
            };

            await _context.Sliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            _context.Sliders.Remove(dbSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
