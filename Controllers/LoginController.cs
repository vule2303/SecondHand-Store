using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Login/Login.cshtml");
        }
    }
}
