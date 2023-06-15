using System.ComponentModel.DataAnnotations;

namespace ASP.NET___fiorello_backend.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
