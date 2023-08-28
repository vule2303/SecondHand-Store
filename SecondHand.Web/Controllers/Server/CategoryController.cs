
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Controllers.Server
{
    public class CategoryController : Controller
    {
        private readonly S2HandDbContext _context;

        public CategoryController(S2HandDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cateList = _context.Categories.ToList();
            return View();
        }
    }
}
