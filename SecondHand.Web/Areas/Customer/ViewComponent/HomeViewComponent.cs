using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

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
            var listCartItem = new List<CartItem>();
            foreach(var product in listSuggestItem)
            {
                CartItem cartObj = new CartItem()
                {
                    Product = product,
                    ProductId = product.Id
                };
                listCartItem.Add(cartObj);
            }
                           

           
            return View(listCartItem);
        }
    }
}
