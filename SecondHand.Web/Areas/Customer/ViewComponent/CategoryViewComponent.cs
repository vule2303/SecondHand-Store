using Microsoft.AspNetCore.Mvc;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;

        public CategoryViewComponent(S2HandDbContext context) {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var a = _context.Categories.ToList();
            return View(a);
        }
    }
}
