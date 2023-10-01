using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.ViewModels;
using SecondHand.Utility;
using System.Security.Claims;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly S2HandDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartVM CartVM { get; set; }

        public CartController(S2HandDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM = new CartVM()
            {
                Order = new SecondHand.Models.Domain.Order(),
                ListCart = _context.CartItems.Where(u => u.UserId == claim.Value)
                            .Include(p => p.Product)
                            .ThenInclude(o => o.productGallery)
                            .Include(p => p.Product.Category)
                            .ToList()
                
            };
            CartVM.Order.Total = 0;
            CartVM.Order.User = _context.ApplicationUsers
                                .FirstOrDefault(u => u.Id == claim.Value);

            foreach(var list in CartVM.ListCart)
            {
                list.price = list.Product.Price;
                CartVM.Order.Total += Convert.ToDecimal(list.price * list.count);
            }


            return View(CartVM);
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _context.CartItems.FirstOrDefault(u => u.Id == cartId);
            var cnt = _context.CartItems.Where(u => u.UserId == cart.UserId).Count();
            _context.CartItems.Remove(cart);
            _context.SaveChanges();

            HttpContext.Session.SetInt32(SD.ssShopingCart, cnt - 1) ;

            return RedirectToAction("Index");
        }
    }
}
