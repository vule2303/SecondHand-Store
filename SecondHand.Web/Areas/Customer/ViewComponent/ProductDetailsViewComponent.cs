using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class ProductDetailsViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;

        public ProductDetailsViewComponent(S2HandDbContext context) { 
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var a = _context.Products.Where(p => p.Status == true)                
                .Include(p => p.productGallery)
                .Include(p=>p.Brand).ToList();
            return View(a);
        }
    }
}
