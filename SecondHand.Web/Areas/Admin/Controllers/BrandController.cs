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
using SecondHand.Models.Models.Domain;

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
        public IActionResult Index(int pg = 1)
        {
            List<Brand>? brandList = _context.Brands.ToList();
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = brandList.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = brandList.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            //return View(cateList);

            return View(data);
            //return View(brandList);
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

                if(model.ImageFile != null)

                {
                    //save image into wwwroot/images/brands
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extention = Path.GetExtension(model.ImageFile.FileName);
                    model.Logo = fileName = fileName + DateTime.Now.ToString("ddMMyyyy") + extention;
                    string path = Path.Combine(wwwRootPath + "/images/brands", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }
                }
                
                _context.Brands.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Tạo mới thành công";
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

                    if (getLogo != null)

                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images/brands", getLogo);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Save the new image
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(updatedBrand.ImageFile.FileName);
                    string extention = Path.GetExtension(updatedBrand.ImageFile.FileName);
                    existingBrand.Logo = fileName = fileName + DateTime.Now.ToString("ddMMyyyy") + extention;

                    string path = Path.Combine(wwwRootPath + "/images/brands", fileName);


                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await updatedBrand.ImageFile.CopyToAsync(fileStream);
                    }
                    TempData["Success"] = "Chỉnh sửa thành công";

                }
                else
                {

                    // If no new image is provided, retain the existing image
                    existingBrand.Logo = existingBrand.Logo;

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
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images/brands", getLogo);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            TempData["Success"] = "Xoá thành công";
            return RedirectToAction("Index");
        }


        // GET: Brand/Search/
        public async Task<IActionResult> Search(string searchTerm)
        {
            IQueryable<Brand> query = _context.Brands;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            List<Brand> searchResults = await query.ToListAsync(); // Lấy kết quả tìm kiếm
            ViewBag.SearchTerm = searchTerm;


            return View("Index", searchResults) ; // Truyền kết quả tìm kiếm vào trang Index
        }
    }
}
