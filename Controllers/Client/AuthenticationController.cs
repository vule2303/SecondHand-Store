using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
 