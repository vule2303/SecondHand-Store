using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;
using SecondHand.Models.ViewModels;


namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FilterController : Controller
    {
        S2HandDbContext _context = new S2HandDbContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SPTheoLoai(int loaiSp)
        {
            List<Product> lstSp = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.productGallery)
                .Where(x => x.Category.Id == loaiSp)
                .OrderBy(x => x.Name).ToList();
            if(lstSp == null || lstSp.Count == 0)
            {
                ViewBag.Empty = "Không có sản phẩm nào!!!";
            }
            var aidi = _context.Categories
                .Where(x => x.Id == loaiSp).ToList();
            ViewBag.TenDanhMuc = aidi;
            return View(lstSp);
        }
        public IActionResult SPTheoBrand(int loaiSp, int pg = 1)
        {
            const int pageSize = 1;
            pg = Math.Max(1, pg);

            List<Product> lstSp = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.productGallery)
                .Where(x => x.Brand.Id == loaiSp)
                .OrderBy(x => x.Name)
                .ToList();

            if (lstSp.Count == 0)
            {
                ViewBag.Empty = "Không có sản phẩm nào!!!";
            }

            var aidi = _context.Brands
                .Where(x => x.Id == loaiSp)
                .ToList();

            ViewBag.TenDanhMuc = aidi;

            var recsCount = lstSp.Count;

            var pager = new Pager(recsCount, pg, pageSize);
            var recSkip = (pg - 1) * pageSize;

            var data = lstSp.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            return View(data);
        }


    }
}
