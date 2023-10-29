using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Text;
using SecondHand.Utility;
using System.Security.Claims;
using SecondHand.Utility.Services;
using Microsoft.AspNetCore.Identity;

namespace MVC_Core.Areas.Customer.Controllers
{
	[Area("Customer")]

	public class SearchController : Controller
	{
		private readonly S2HandDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SearchController(S2HandDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Search(string searchTerm)
		{

			IQueryable<Product> query = _context.Products;
			if (!string.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(p => p.Name.Contains(searchTerm));
			}

			var products = await query
				.Include(p => p.Brand)
				.Include(p => p.Category)
				.Include(p => p.productGallery)
				.ToListAsync();
         
			return View("Index", products);
		}
        [HttpPost]
        public async Task<IActionResult> Search(CartItem CartObject)
        {

            CartObject.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdenity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdenity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.UserId = claim.Value;

                CartItem cartFromDb = _context.CartItems.Include(db => db.Product).FirstOrDefault(u => u.UserId == CartObject.UserId && u.ProductId == CartObject.ProductId);

                if (cartFromDb == null)
                {
                    _context.CartItems.Add(CartObject);
                }
                else
                {
                    cartFromDb.count += CartObject.count;
                    //_context.CartItems.Update(cartFromDb);
                }
                _context.SaveChanges();

                var count = _context.CartItems
                    .Where(c => c.UserId == CartObject.UserId)
                    .Select(m => m.count)
                    .Count();
                HttpContext.Session.SetInt32(SD.ssShopingCart, count);

                return RedirectToAction("Index");
            }
            else
            {
                var product = await _context.Products
               .Include(p => p.Brand)
               .Include(p => p.Category)
               .Include(p => p.productGallery)
               .FirstOrDefaultAsync(m => m.Id == CartObject.ProductId);
                CartItem cartObj = new CartItem()
                {
                    Product = product,
                    ProductId = product.Id
                };
                if (product == null)
                {
                    return NotFound();
                }
                return View(cartObj);
            }
        }
        public bool Check(int productId, string userId)
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
