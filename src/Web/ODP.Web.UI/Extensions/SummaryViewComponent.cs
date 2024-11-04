using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ODP.Web.UI.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
