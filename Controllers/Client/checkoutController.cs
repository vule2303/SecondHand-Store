using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class checkoutController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Checkout/Checkout.cshtml");
        }
    }
}
