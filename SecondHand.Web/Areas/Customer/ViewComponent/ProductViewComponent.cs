using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;

        public ProductViewComponent(S2HandDbContext context) {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var a = _context.Products.Where(p => p.Status == true)
                .Include(p => p.Brand)
                .Include(p => p.Category)                
                .Include(p => p.productGallery).ToList();

           
            return View(a);
        }
    }
}
