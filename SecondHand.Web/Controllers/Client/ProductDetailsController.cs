using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class ProductDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/ProductDetails/ProductDetails.cshtml");
        }
    }
}
