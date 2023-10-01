using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NuGet.Protocol;
using SecondHand.DataAccess.Data;
using SecondHand.Models;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;
using SecondHand.Utility;
using SecondHand.Utility.Services;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly S2HandDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(S2HandDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }


        public  IActionResult Index()
        {
            var claimsIdenity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdenity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                var count = _context.CartItems
                   .Where(c => c.UserId == claim.Value)
                   .Select(m => m.count)
                   .ToList()
                   .Count();
                HttpContext.Session.SetInt32(SD.ssShopingCart, count);
            }
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.productGallery)
                .FirstOrDefaultAsync(m => m.Id == id);
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
        [HttpPost]
        public async Task<IActionResult> Details(CartItem CartObject)
        {
            CartObject.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdenity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdenity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.UserId = claim.Value;

                CartItem cartFromDb = _context.CartItems.Include(db => db.Product).FirstOrDefault(u => u.UserId == CartObject.UserId && u.ProductId == CartObject.ProductId);
            
                if(cartFromDb == null)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}