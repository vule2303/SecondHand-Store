using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Client
{
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Personal/Profile.cshtml");
        }
    }
}
