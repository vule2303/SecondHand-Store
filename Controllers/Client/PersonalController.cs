using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Personal/Profile.cshtml");
        }
    }
}
