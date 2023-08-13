using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Client/Login/Login.cshtml");
        }
    }
}
