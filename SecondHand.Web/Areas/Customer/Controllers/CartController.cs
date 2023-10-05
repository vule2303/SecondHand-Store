using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;
using SecondHand.Models.ViewModels;
using SecondHand.Utility;
using SecondHand.Utility.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly S2HandDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVnPayService _vnPayService;
        [BindProperty]
        public CartVM CartVM { get; set; }

        public CartController(S2HandDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IVnPayService vnPayService)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _vnPayService = vnPayService;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM = new CartVM()
            {
                Order = new Order(),
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
        public IActionResult CheckOut()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            CartVM = new CartVM()
            {
                Order = new Order(),
                ListCart = _context.CartItems.Where(c => c.UserId == claim.Value)
                            .Include(p => p.Product)
                            .ThenInclude(o => o.productGallery)
                            .Include(p => p.Product.Category)
                            .ToList()
            };

            CartVM.Order.Total = 0;
            CartVM.Order.User = _context.ApplicationUsers
                                .FirstOrDefault(c => c.Id == claim.Value);

            foreach (var list in CartVM.ListCart)
            {
                list.price = list.Product.Price;
                CartVM.Order.Total += Convert.ToDecimal(list.price * list.count);
            }

            CartVM.Order.Name = CartVM.Order.User.UserName;
            CartVM.Order.PhoneNumber = CartVM.Order.User.PhoneNumber;

            return View(CartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            CartVM.Order.User = _context.ApplicationUsers.FirstOrDefault(c => c.Id == claim.Value);

            CartVM.ListCart = _context.CartItems.Include(p => p.Product).Where(c => c.UserId == claim.Value).ToList();

            //tao ma don hang ngau nhien
            Random random = new Random();
            int randomNumber = random.Next(100000000, 999999999);
            CartVM.Order.OrderCode = "DH" + randomNumber.ToString();
            CartVM.Order.PaymentStatus = SD.PaymentStatusPending;
            CartVM.Order.OrderStatus = SD.OrderStatusPending;
            CartVM.Order.UserId = claim.Value;
            CartVM.Order.OrderDate = DateTime.Now;
      
            _context.Orders.Add(CartVM.Order);
            _context.SaveChanges();
            CartVM.Order.Total = 0;
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();

            foreach (var item in CartVM.ListCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    OrderId = CartVM.Order.Id,
                    Price = item.Product.Price,
                    Count = item.count,
               };

                CartVM.Order.Total += (orderDetail.Count * orderDetail.Price);
                _context.OrderDetail.Add(orderDetail);

            }

            _context.CartItems.RemoveRange(CartVM.ListCart);
            _context.SaveChanges();
            HttpContext.Session.SetInt32(SD.ssShopingCart, 0);


            if ( CartVM.Order.PaymentMethod == SD.PaymentMethodCod)
            {
              
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.Order.Id });
            }
            else if(CartVM.Order.PaymentMethod == SD.PaymentMethodVnPay)
            {
                //process the payment
                var url = _vnPayService.CreatePaymentUrl(CartVM.Order, HttpContext);

                return Redirect(url);
            }
            else
            {
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.Order.Id });

            }


        }

        public IActionResult OrderConfirmation(int id)
        {
            var listOdered = _context.Orders
                .Include(d => d.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(x => x.productGallery)
                .Include(u => u.User)
                .FirstOrDefault(x => x.Id == id);
            return View(listOdered);
        }

        public IActionResult CreatePaymentUrl(Order model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if(response.Success == true)
            {
                var orderFinded = _context.Orders.Where(o => o.Id == response.OrderId).FirstOrDefault();
                if(orderFinded != null)
                {
                    orderFinded.PaymentStatus = SD.PaymentStatusApproved;
                    orderFinded.OrderStatus = SD.OrderStatusPending;
                    _context.SaveChanges();
                    return RedirectToAction("OrderConfirmation", "Cart", new { id = orderFinded.Id }); ;

                }

                return Json(response);

            }
            return Json(response);
        }
    }
}
