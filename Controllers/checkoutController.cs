using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers
{
    public class checkoutController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/checkout/checkout.cshtml");
        }
    }
}
