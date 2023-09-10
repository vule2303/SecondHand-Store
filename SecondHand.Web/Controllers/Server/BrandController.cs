using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.Models.Domain;
using SecondHand.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;


namespace MVC_Core.Controllers.Server
{
    public class BrandController : Controller
    {
        private readonly S2HandDbContext _context; 
        public BrandController(S2HandDbContext context)
        {
            _context = context;
        }

        // GET: BrandController
        public IActionResult Index()
        {
            List<Brand>? brandList = _context.Brands.ToList();  
            return View(brandList);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            var model = new Brand();
            return View(model);
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand model)
        {
            if (ModelState.IsValid)
            {
                _context.Brands.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BrandController/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Brand updatedBrand)
        {
            if (id != updatedBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBrand = _context.Brands.Find(id);
                if (existingBrand == null)
                {
                    return NotFound();
                }

                existingBrand.Name = updatedBrand.Name;
                existingBrand.Logo = updatedBrand.Logo;
                existingBrand.Description = updatedBrand.Description;

                _context.Update(existingBrand);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedBrand);

        }
        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Brand collection)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
