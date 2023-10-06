using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.ViewModels;


namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FilterController : Controller
    {
        S2HandDbContext _context = new S2HandDbContext();
        public IActionResult Index()
        {
            var a = _context.Products.ToList();
            return View(a);
        }
        public ActionResult ProductCategory(int id)
        {
            var listpro = _context.Categories
                .Include(p=>p.Products)
                .Where(p => p.Id==id)
            .ToList();
                   


            return View(listpro);
        }
        public IActionResult ProductDetail(int id)
        {
            var SanPham = _context.Products.SingleOrDefault(p => p.Id == id);
            var anhSP = _context.ProductImages.Where(p => p.Id == id).ToList();
            var TH = _context.Brands.SingleOrDefault(p => p.Id == id);
            var proDetailViewModel = new ProCate
            {
                DanhMuc = SanPham,    
                ThuongHieu = TH,
                AnhSP = anhSP
            };
            var productNames = _context.Categories
                  .Where(p => p.Id == id).Select(c => c.Name).ToList();

            if (productNames.Any())
            {
                ViewBag.ProductNames = productNames;
            }
            else
            {
                ViewBag.ProductNames = new List<string> { "Không có sản phẩm nào phù hợp" };
            }
            ViewBag.ThuongHieu = TH;
            return View(proDetailViewModel);
        }

        public ActionResult BrandCategory(int id)
        {
            var listpro = _context.Brands.Where(p => p.Id == id).ToList();
            var productNames = _context.Brands
                 .Where(p => p.Id==id).Select(c => c.Name).ToList();

            if (productNames.Any())
            {
                ViewBag.BrandName = productNames;
            }
            else
            {
                ViewBag.BrandName = new List<string> { "Không có sản phẩm nào phù hợp" };
            }
            return View(listpro);
        }

    }
}
