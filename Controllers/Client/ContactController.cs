using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Client/Contact/Index.cshtml");
        }
    }
}
