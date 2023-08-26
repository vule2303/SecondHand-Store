using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Client
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Client/Dashboard/Display");
        }
    }
}
