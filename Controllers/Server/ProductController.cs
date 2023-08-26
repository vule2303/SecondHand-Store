using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Server
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
