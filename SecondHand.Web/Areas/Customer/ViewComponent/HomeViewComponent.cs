using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Utility.Services;
using System.Security.Claims;

namespace MVC_Core.Areas.Customer.ViewComponents
{
    [Area("Customer")]

    public class HomeViewComponent : ViewComponent
    {
        private readonly S2HandDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		public HomeViewComponent(S2HandDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            var listSuggestItem = _context.Products.Where(p => p.Status == true)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.productGallery).ToList();
            var listCartItem = new List<CartItem>();
            var isProductInCartList = new List<bool>();
            foreach(var product in listSuggestItem)
            {
                CartItem cartObj = new CartItem()
                {
                    Product = product,
                    ProductId = product.Id
                };
				if (_signInManager.IsSignedIn(UserClaimsPrincipal))
				{
					var claimsIdentity = (ClaimsIdentity)User.Identity;
					var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
					var check = Check(product.Id, claim.Value);

                    isProductInCartList.Add(check);

				}
				listCartItem.Add(cartObj);
			}

            ViewBag.IsProductInCartList = isProductInCartList;
            HttpContext.Session.SetObject("isProductInCartList", isProductInCartList);
			return View(listCartItem);
        }
        public bool Check (int productId, string userId)
        {           	
				var check = _context.CartItems.Where(a => a.UserId == userId && a.ProductId == productId).FirstOrDefault();
                if (check != null)
                {
                    return true;
                }
                else
                {
                    return false;

                }
		}
    }
}
