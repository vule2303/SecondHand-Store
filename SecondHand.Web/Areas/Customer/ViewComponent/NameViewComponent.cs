using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;


namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class NameViewComponent : ViewComponent
    {
        S2HandDbContext _context = new S2HandDbContext();
        public IViewComponentResult Invoke()
        {
            var products = _context.Categories
                .Where(p => p.Status == true)
                .Include(p => p.Products).ToList();

            return View(products);
        }

    }
}
