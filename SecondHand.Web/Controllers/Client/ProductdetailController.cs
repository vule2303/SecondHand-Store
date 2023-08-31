using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class ProductdetailController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Productdetail/Productdetail.cshtml");
        }
    }
}
