using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Server
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
