using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Cart/Cart.cshtml");
        }
    }
}
