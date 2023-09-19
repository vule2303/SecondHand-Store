using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FilterController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Filter/Filter.cshtml");
        }
    }
}
