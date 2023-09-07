using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Server
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
