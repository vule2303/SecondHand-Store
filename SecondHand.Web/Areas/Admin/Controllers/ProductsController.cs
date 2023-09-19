using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/san-pham/[action]/{id?}")]
    public class ProductsController : Controller
    {

        private readonly S2HandDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductsController(S2HandDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.productGallery).ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.productGallery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {
                var tempProduct = _context.Products.Add(product).Entity;
                _context.SaveChanges();
                if (product.MultipleImages != null)
                {
                    string folder = "/images/products/";
                    product.ProductImages = new List<ProductImages>();

                    foreach (var file in product.MultipleImages)
                    {
                        var images = new ProductImages()
                        {
                            ProductId = tempProduct.Id,
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        };
                        _context.ProductImages.Add(images);
                    }
                }
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tạo mới thành công";
                return RedirectToAction("Index");
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            TempData["Error"] = "Tạo mới không thành công";
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.productGallery).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.MultipleImages != null)
                    {
                        var productImages = _context.ProductImages.Where(x => x.ProductId == product.Id).ToList();
                        foreach (var img in productImages)
                        {
                            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/products/", img.URL);
                            if (System.IO.File.Exists(imagePath))
                                System.IO.File.Delete(imagePath);

                            _context.ProductImages.Remove(img);
                        }

                        string folder = "/images/products/";
                        product.ProductImages = new List<ProductImages>();

                        foreach (var file in product.MultipleImages)
                        {
                            var images = new ProductImages()
                            {
                                ProductId = product.Id,
                                Name = file.Name,
                                URL = await UploadImage(folder, file)
                            };
                            _context.ProductImages.Add(images);
                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            TempData["Error"] = "Cập nhật không thành công";
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.productGallery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                TempData["Error"] = "Xoá sản phẩm không thành công";

                return NotFound();
            }
            var productImages = _context.ProductImages.Where(p => p.ProductId == id).ToList();

            //Xoá toàn bộ hình ảnh của sản phẩm

            foreach (var img in productImages)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/products/", img.URL);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

                _context.ProductImages.Remove(img);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Xoá sản phẩm thành công";
            return RedirectToAction(nameof(Index));


        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath + folderPath, fileName);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return fileName;
        }
    }
}
