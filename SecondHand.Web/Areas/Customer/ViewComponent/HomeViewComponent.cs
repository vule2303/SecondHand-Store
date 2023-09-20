using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    [Area("Customer")]
    public class HomeViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;
        public HomeViewComponent(S2HandDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var listSuggestItem = _context.Products.Where(p => p.Status == true)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.productGallery).ToList();

            return View(listSuggestItem);
        }
    }
}
