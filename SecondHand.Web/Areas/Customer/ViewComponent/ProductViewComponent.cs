using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using System.Security.Claims;
using SecondHand.Models.Models.Domain;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;


        public ProductViewComponent(S2HandDbContext context) {
            _context = context;
           
        }
        public IViewComponentResult Invoke(int pg = 1)
        {
            var a = _context.Products.Where(p => p.Status == true)
                .Include(p => p.Brand)
                .Include(p => p.Category)                
                .Include(p => p.productGallery).ToList();

            const int pageSize = 8;
            if (pg < 1)
                pg = 1;

            int recsCount = a.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = a.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            //return View(cateList);

            return View(data);

            //return View(a);
        }
       
    }
}
