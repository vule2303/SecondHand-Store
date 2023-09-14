using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class FavoritesController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Favorites/Favorites.cshtml");
        }
    }
}
