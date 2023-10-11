
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/danh-muc/[action]/{id?}")]

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

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();

                TempData["Success"] = "Tạo mới thành công";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Tạo mới không thành công";
            return View();
        }
        public IActionResult Update(int? id)
        {


            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _context.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }


            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Update(Category obj)
        {

            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["Success"] = "Cập nhật thành công";

                return RedirectToAction("Index");
            }
            TempData["Error"] = "Cập nhật không thành công";

            return View();
        }
        public IActionResult Delete(int? id)
        {


            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _context.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();

            }



            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            obj = _context.Categories.Find(obj.Id);
            if (obj == null)
            {
                TempData["Error"] = "Xoá không thành công";
                return NotFound();
            }
            _context.Categories.Remove(obj);

            _context.SaveChanges();
            TempData["Success"] = "Xoá thành công";
            return RedirectToAction("Index");

        }
        // GET: Category/Search/
        public async Task<IActionResult> Search(string searchTerm)
        {
            IQueryable<Category> query = _context.Categories;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            List<Category> searchResults = await query.ToListAsync(); 
            ViewBag.SearchTerm = searchTerm;


            return View("Index", searchResults); 
        }
    }
}
