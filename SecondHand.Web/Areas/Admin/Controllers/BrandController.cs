using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.Models.Domain;
using SecondHand.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.IO;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/thuong-hieu/[action]/{id?}")]
    public class BrandController : Controller
    {

        private readonly S2HandDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(S2HandDbContext context, IWebHostEnvironment hostingEnviroment)
        {
            _context = context;
            _webHostEnvironment = hostingEnviroment;
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
        public async Task<ActionResult> Create(Brand model)
        {


            if (ModelState.IsValid)
            {

                //save image into wwwroot/images
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extention = Path.GetExtension(model.ImageFile.FileName);
                model.Logo = fileName = fileName + DateTime.Now.ToString("ddMMyyyy") + extention;
                string path = Path.Combine(wwwRootPath + "/images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
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
        public async Task<ActionResult> Edit(int id, Brand updatedBrand)
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

                if (updatedBrand.ImageFile != null) // Check if a new image is provided
                {
                    // Delete the existing image
                    var getLogo = existingBrand.Logo;
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", getLogo);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    // Save the new image
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(updatedBrand.ImageFile.FileName);
                    string extention = Path.GetExtension(updatedBrand.ImageFile.FileName);
                    updatedBrand.Logo = fileName = fileName + DateTime.Now.ToString("ddMMyyyy") + extention;
                    string path = Path.Combine(wwwRootPath + "/images", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await updatedBrand.ImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    // If no new image is provided, retain the existing image
                    updatedBrand.Logo = existingBrand.Logo;
                }

                existingBrand.Name = updatedBrand.Name;
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
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public ActionResult PostDelete(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            //delete image in wwroot
            var getLogo = brand.Logo;
            if (getLogo != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", getLogo);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            TempData["Error"] = "Xoá không thành công";
            return RedirectToAction("Index");
        }
    }
}
