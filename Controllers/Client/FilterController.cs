using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class FilterController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Filter/Filter.cshtml");
        }
    }
}
