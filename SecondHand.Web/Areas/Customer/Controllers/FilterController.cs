using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;

using SecondHand.Models.Domain;
using System.Collections.Generic;


namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FilterController : Controller
    {

        private readonly S2HandDbContext _db;

        public FilterController(S2HandDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var a = _context.Products.ToList();
            return View(a);
        }

        public IActionResult SPTheoLoai(int loaiSp)
        {
            List<Product> lstSp = _db.Products
                .Include(x=>x.Category)
                .Include(x=>x.Brand)
                .Include(x=>x.productGallery)
                .Where(x=> x.Category.Id == loaiSp)
                .OrderBy(x=>x.Name).ToList();
           
            var aidi = _db.Categories
                .Where(x=>x.Id == loaiSp).ToList();
            ViewBag.TenDanhMuc = aidi;
            return View(lstSp);
        }
        public IActionResult SPTheoBrand(int loaiSp)
        {
            List<Product> lstSp = _db.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.productGallery)
                .Where(x => x.Brand.Id == loaiSp)
                .OrderBy(x => x.Name).ToList();
            
            var aidi = _db.Brands
                .Where(x => x.Id == loaiSp).ToList();
            ViewBag.TenDanhMuc = aidi;
            return View(lstSp);
        }




        
       

        

    }
}
