using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class checkoutController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Checkout/Checkout.cshtml");
        }
    }
}
