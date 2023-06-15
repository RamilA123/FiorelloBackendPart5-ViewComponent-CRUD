using ASP.NET___fiorello_backend.Services.Interfaces;
using ASP.NET___fiorello_backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET___fiorello_backend.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM datas = _layoutService.GetSettingDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
