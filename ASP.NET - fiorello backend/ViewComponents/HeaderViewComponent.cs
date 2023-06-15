using ASP.NET___fiorello_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET___fiorello_backend.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = _layoutService.GetSettingDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
