using Microsoft.AspNetCore.Mvc;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class BranddViewComponent : ViewComponent
    {
        S2HandDbContext _context = new S2HandDbContext();
        public IViewComponentResult Invoke()
        {
            var pro = _context.Brands.ToList();

            return View(pro);
        }
    }
}
