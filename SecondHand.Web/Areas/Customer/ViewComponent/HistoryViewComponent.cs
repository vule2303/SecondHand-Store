using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Core.Areas.Customer.Repository;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class HistoryViewComponent: ViewComponent
    {
        /*private readonly IHistoryProductRepository _history;*/
        S2HandDbContext _context = new S2HandDbContext();
        /*public HistoryViewComponent(IHistoryProductRepository history) {
            _history = history;
        }*/
        public IViewComponentResult Invoke()
        {
/*            var list = _history.GetAllHistory().OrderBy(x=>x.Name);
*/            var listt = _context.Users
                .Include(x=>x.Orders)                
                .ToList();
            return View(listt);
        }
    }
}
