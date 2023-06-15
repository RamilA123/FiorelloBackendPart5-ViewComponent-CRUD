using ASP.NET___fiorello_backend.Models;
namespace ASP.NET___fiorello_backend.ViewModels
{
    public class SliderVM
    {
        public SliderInfo SliderInfo { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
    }
}
