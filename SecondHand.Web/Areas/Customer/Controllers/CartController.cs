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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SecondHand.DataAccess.Migrations;
using System.Globalization;
using Microsoft.Build.ObjectModelRemoting;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly S2HandDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVnPayService _vnPayService;
        private readonly IWebHostEnvironment _hostEnvironment;


        [BindProperty]
        public CartVM CartVM { get; set; }
        public static bool CheckPomotionIsUsed { get; set; }
        public CartController(S2HandDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IVnPayService vnPayService, IWebHostEnvironment hostEnvironment)
        {

            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _vnPayService = vnPayService;
            _hostEnvironment = hostEnvironment;
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

            foreach (var list in CartVM.ListCart)
            {
                list.price = list.Product.Price;
                CartVM.Order.Subtotal += Convert.ToDecimal(list.price * list.count);
            }

            var getPromotion = HttpContext.Session.GetString("promotionCode");

            CartVM.Order.Total = CartVM.Order.Subtotal;

            if (!string.IsNullOrEmpty(getPromotion))
            {
                var promotion = _context.Promotions.Where(c => c.Code == getPromotion).FirstOrDefault();
                ViewData["getPromotion"] = promotion;
                if (promotion != null)
                {
                    var checkPromotionIsUsed = _context.Orders.Where(p => p.PromotionId == promotion.Id && p.UserId == claim.Value).FirstOrDefault();

                    if (checkPromotionIsUsed == null)
                    {
                        CheckPomotionIsUsed = false;

                        if (promotion.EndDate != null && promotion.EndDate > DateTime.Now)
                        {
                            if (CartVM.Order.Total >= promotion.MiniumOrderAmount)
                            {
                                ViewBag.ApplyDiscount = true;
                                var countValue = CalculateDiscount(CartVM.Order.Total, promotion);
                                CartVM.Order.Discount = countValue;
                                CartVM.Order.Total -= countValue;
                            }
                            else
                            {
                                ViewBag.ApplyDiscount = false;
                            }
                        }
                        else
                        {
                            ViewBag.DiscountExpired = true;
                        }
                    }

                    else
                    {
                        CheckPomotionIsUsed = true;
                        ViewBag.PromotionIsUsed = true;
                    }
                }
                else
                {
                    ViewBag.DiscountExist = false;
                }
            }
            return View(CartVM);
        }
        [HttpPost]
        public IActionResult ApplyPromotion(string promotionCode)
        {
            if (!string.IsNullOrEmpty(promotionCode))
            {
                HttpContext.Session.SetString("promotionCode", promotionCode);
            }
            return RedirectToAction("Index");
        }

        private decimal CalculateDiscount(decimal total, Promotion promotion)
        {
            // Thực hiện tính toán giảm giá dựa trên loại giảm giá và giá trị giảm giá trong promotion
            // Đây là nơi bạn triển khai logic tính toán giảm giá tùy theo yêu cầu của ứng dụng của bạn.
            // Ví dụ: Nếu DiscountType là "Percentage" thì giảm giá theo tỷ lệ, nếu là "FixedAmount" thì giảm giá một số tiền cố định.

            if (promotion.DiscountType == SD.PromotionPercent)
            {
                return total * promotion.DiscountValue / 100;
            }
            else if (promotion.DiscountType == SD.PromotionAmount)
            {

                return promotion.DiscountValue;
            }
            else
                return 0; // Mặc định không có giảm giá
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _context.CartItems.FirstOrDefault(u => u.Id == cartId);
            var cnt = _context.CartItems.Where(u => u.UserId == cart.UserId).Count();
            _context.CartItems.Remove(cart);
            _context.SaveChanges();

            HttpContext.Session.SetInt32(SD.ssShopingCart, cnt - 1);

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


            CartVM.Order.User = _context.ApplicationUsers
                                .FirstOrDefault(c => c.Id == claim.Value);

            foreach (var list in CartVM.ListCart)
            {
                list.price = list.Product.Price;
                CartVM.Order.Subtotal += Convert.ToDecimal(list.price * list.count);
            }
            CartVM.Order.Total = CartVM.Order.Subtotal;
            if (CheckPomotionIsUsed == false)
            {
                var getPromotion = HttpContext.Session.GetString("promotionCode");

                if (!string.IsNullOrEmpty(getPromotion))
                {
                    var promotion = _context.Promotions.Where(c => c.Code == getPromotion).FirstOrDefault();
                    if (promotion != null)
                    {
                        var countValue = CalculateDiscount(CartVM.Order.Total, promotion);
                        CartVM.Order.Discount = countValue;
                        CartVM.Order.Total -= countValue;
                    }
                }
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
            var getPromotionCode = HttpContext.Session.GetString("promotionCode");
            var promotion = _context.Promotions.Where(c => c.Code == getPromotionCode).FirstOrDefault();
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

            if (promotion != null)
            {
                CartVM.Order.PromotionId = promotion.Id;
            }

            _context.Orders.Add(CartVM.Order);
            _context.SaveChanges();
            CartVM.Order.Total = 0;
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();
            decimal subTotal = 0;
            foreach (var item in CartVM.ListCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    OrderId = CartVM.Order.Id,
                    Price = item.Product.Price,
                    Count = item.count,
                };
                //item.Product.Status = false;
                CartVM.Order.Subtotal += (orderDetail.Count * orderDetail.Price);
                _context.OrderDetail.Add(orderDetail);
            }
            var listOrder = _context.ApplicationUsers.Include(o => o.Orders).Where(o => o.Id == CartVM.Order.UserId);
            subTotal = CartVM.Order.Subtotal;
            var feeShip = CartVM.Order.FeeShipping;
            CartVM.Order.Total = subTotal + feeShip - CartVM.Order.Discount;
            _context.CartItems.RemoveRange(CartVM.ListCart);
            _context.SaveChanges();
            HttpContext.Session.SetInt32(SD.ssShopingCart, 0);
            if (CartVM.Order.PaymentMethod == SD.PaymentMethodCod)
            {
                SendOrderEmail(claim.Value, CartVM.Order.Id);
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.Order.Id });
            }
            else if (CartVM.Order.PaymentMethod == SD.PaymentMethodVnPay)
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
        public void SendOrderEmail(string Userid, int orderId)
        {
            var getUser = _context.ApplicationUsers.Find(Userid);
            var getOrder = _context.Orders.Find(orderId);
            var listOrders = getOrder.OrderDetails.ToList();
            var listOdered = _context.Orders
                .Include(d => d.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(x => x.productGallery)
                .Include(u => u.User)
                .FirstOrDefault(x => x.Id == orderId);
            int count = 1;
            var PathToFile = _hostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Templates" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates" + Path.DirectorySeparatorChar.ToString() + "confirm_Order.html";
            var subject = "Thông báo đơn hàng";
            string htmlBody = "";
            using (StreamReader streamReader = System.IO.File.OpenText(PathToFile))
            {
                htmlBody = streamReader.ReadToEnd();
            }
            //get time Vetnam
            CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
            DateTime now = DateTime.Now;
            var name = getUser.FirstName + " " + getUser.LastName;
            var address = getOrder.Address;
            var phone = getOrder.PhoneNumber;
            var orderCode = getOrder.OrderCode;
            var dateOrder = getOrder.OrderDate;
            var paymentMethod = getOrder.PaymentMethod;
            var status = getOrder.OrderStatus;
            var subTotal = SD.ConvertToCurrencyFormat(getOrder.Subtotal);
            var total = SD.ConvertToCurrencyFormat(getOrder.Total);
            var feeShipping = SD.ConvertToCurrencyFormat(getOrder.FeeShipping);
            var discount = SD.ConvertToCurrencyFormat(getOrder.Discount);

            var listProduct = "";
            foreach(var item in listOdered.OrderDetails)
            {
                listProduct += $"<div\r\nclass=\"row mb-2 mb-sm-0 py-25\"\r\nstyle=\"\r\nbox-sizing: border-box;\r\ndisplay: flex;\r\n-ms-flex-wrap: wrap;\r\nflex-wrap: wrap;\r\nmargin-right: -15px;\r\nmargin-left: -15px;\r\nmargin-bottom: 0 !important;\r\npadding-bottom: 0.75rem !important;\r\npadding-top: 0.75rem !important;\r\n\"\r\n>\r\n<div\r\nclass=\"d-none d-sm-block col-1\"\r\nstyle=\"\r\nbox-sizing: border-box;\r\nposition: relative;\r\nwidth: 100%;\r\npadding-right: 15px;\r\npadding-left: 15px;\r\n-ms-flex: 0\r\n0\r\n8.333333%;\r\nflex: 0 0\r\n8.333333%;\r\nmax-width: 8.333333%;\r\ndisplay: block !important;\r\n\"\r\n>\r\n{count++}\r\n</div>\r\n<div\r\nclass=\"col-9 col-sm-5\"\r\nstyle=\"\r\nbox-sizing: border-box;\r\nposition: relative;\r\nwidth: 100%;\r\npadding-right: 15px;\r\npadding-left: 15px;\r\n-ms-flex: 0\r\n0\r\n41.666667%;\r\nflex: 0 0\r\n41.666667%;\r\nmax-width: 41.666667%;\r\n\"\r\n>\r\n{item.Product.Name}\r\n</div>\r\n<div\r\nclass=\"d-none d-sm-block col-2\"\r\nstyle=\"\r\nbox-sizing: border-box;\r\nposition: relative;\r\nwidth: 100%;\r\npadding-right: 15px;\r\npadding-left: 15px;\r\n-ms-flex: 0\r\n0\r\n16.666667%;\r\nflex: 0 0\r\n16.666667%;\r\nmax-width: 16.666667%;\r\ndisplay: block !important;\r\n\"\r\n>\r\n1\r\n</div>\r\n<div\r\nclass=\"d-none d-sm-block col-2 text-95\"\r\nstyle=\"\r\nbox-sizing: border-box;\r\nposition: relative;\r\nwidth: 100%;\r\npadding-right: 15px;\r\npadding-left: 15px;\r\n-ms-flex: 0\r\n0\r\n16.666667%;\r\nflex: 0 0\r\n16.666667%;\r\nmax-width: 16.666667%;\r\ndisplay: block !important;\r\n\"\r\n>\r\n{item.Product.Color}\r\n</div>\r\n<div\r\nclass=\"col-2 text-secondary-d2\"\r\nstyle=\"\r\nbox-sizing: border-box;\r\nposition: relative;\r\nwidth: 100%;\r\npadding-right: 15px;\r\npadding-left: 15px;\r\n-ms-flex: 0\r\n0\r\n16.666667%;\r\nflex: 0 0\r\n16.666667%;\r\nmax-width: 16.666667%;\r\n\"\r\n>\r\n{SD.ConvertToCurrencyFormat(item.Product.Price)}\r\n</div>\r\n</div>";
            }

            try
            {
            string messageBody = string.Format(htmlBody, subject, name, address, phone, orderCode, dateOrder, status, listProduct, paymentMethod, subTotal, discount, feeShipping, total);
            // Tiếp tục xử lý messageBody và gửi email
            _emailSender.SendEmailAsync(getUser.Email, subject, messageBody);
            }
            catch (FormatException ex)
            {
            // Xử lý lỗi định dạng cụ thể
            // In ra thông báo lỗi hoặc thực hiện các hành động cần thiết để xử lý lỗi
                Console.WriteLine("Lỗi định dạng: " + ex.Message);
            }
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response.Success == true)
            {
                var orderFound = _context.Orders.Where(o => o.Id == response.OrderId).FirstOrDefault();
                if (orderFound != null)
                {
                    orderFound.PaymentStatus = SD.PaymentStatusApproved;
                    orderFound.OrderStatus = SD.OrderStatusPending;
                    _context.SaveChanges();
                    SendOrderEmail(orderFound.UserId, orderFound.Id);
                    return RedirectToAction("OrderConfirmation", "Cart", new { id = orderFound.Id }); ;
                }
                return Json(response);

            }
            return Json(response);
        }
    }
}
