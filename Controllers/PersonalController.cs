using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers
{
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Profile/Personal.cshtml");
        }
    }
}
