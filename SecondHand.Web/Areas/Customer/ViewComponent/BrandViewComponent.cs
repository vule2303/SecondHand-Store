using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using SecondHand.DataAccess.Data;



namespace MVC_Core.Areas.Customer.ViewComponents
{
    [Area("Customer")]
    public class BrandViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;
        public BrandViewComponent(S2HandDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var pro = _context.Brands.ToList();

            return View(pro);
        }
    }
}
