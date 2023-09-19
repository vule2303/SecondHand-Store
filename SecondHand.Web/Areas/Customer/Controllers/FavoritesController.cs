using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FavoritesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
