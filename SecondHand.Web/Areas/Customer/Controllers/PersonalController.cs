using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
