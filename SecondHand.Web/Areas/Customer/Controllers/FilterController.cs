﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.ViewModels;
using SecondHand.Utility;
using SecondHand.Utility.Services;
using System.Security.Claims;
namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class FilterController : Controller
    {
        private readonly S2HandDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public FilterController(S2HandDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SPTheoLoai(int loaiSp)
        {
            List<Product> lstSp = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.productGallery)
                .Where(x => x.Category.Id == loaiSp)
                .OrderBy(x => x.Name).ToList();
            if(lstSp == null || lstSp.Count == 0)
            {
                ViewBag.Empty = "Không có sản phẩm nào!!!";
            }
            var aidi = _context.Categories
                .Where(x => x.Id == loaiSp).ToList();
            ViewBag.TenDanhMuc = aidi;

            var listCartItem = new List<CartItem>();
            var isProductInCartList = new List<bool>();
            foreach (var product in lstSp)
            {
                CartItem cartObj = new CartItem()
                {
                    Product = product,
                    ProductId = product.Id
                };
                if (_signInManager.IsSignedIn(User))
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    var check = Check(product.Id, claim.Value);

                    isProductInCartList.Add(check);

                }
                listCartItem.Add(cartObj);
            }
            HttpContext.Session.SetObject("isProductInCartCategory", isProductInCartList);
            return View(lstSp);
        }
        [HttpPost]
        public async Task<IActionResult> SPTheoLoai(CartItem CartObject)
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

                return RedirectToAction("SPTheoLoai"/*, new { id = CartVM.Order.Id })*/);
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
        public IActionResult SPTheoBrand(int loaiSp)
        {
            List<Product> lstSp = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.productGallery)
                .Where(x => x.Brand.Id == loaiSp)
                .OrderBy(x => x.Name).ToList();
            if (lstSp == null || lstSp.Count == 0)
            {
                ViewBag.Empty = "Không có sản phẩm nào!!!";
            }
            var aidi = _context.Brands
                .Where(x => x.Id == loaiSp).ToList();
            ViewBag.TenDanhMuc = aidi;
            return View(lstSp);
        }
    }
}
