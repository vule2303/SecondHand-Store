using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Client
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Client/Contact/Index.cshtml");
        }
    }
}
