using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Client
{
    public class checkoutController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Client/Checkout/Checkout.cshtml");
        }
    }
}
