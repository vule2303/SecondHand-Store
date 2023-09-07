
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

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
            List<Category>? cateList = _context.Categories.ToList();
            return View(cateList);
        }
        public IActionResult Create() 
        {
            var parentCategory = _context.Categories
                .Where(x => x.ParentId == null)
                .ToList();

            ViewBag.ParentCategories = parentCategory;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            
            if(ModelState.IsValid){
                _context.Categories.Add(obj);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View();
        }
    }
}
