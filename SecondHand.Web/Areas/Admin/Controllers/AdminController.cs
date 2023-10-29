using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.ViewModels;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/trang-chu/{action}")]
    [Authorize(Policy = "AllowEdit")]
    public class AdminController : Controller
    {
        private readonly S2HandDbContext _context;
        public AdminController(S2HandDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            decimal totalSales = 0;

            var getAnalyst = _context.Orders.Select(x => new { x.Id, x.Total }).ToList();

            // Tạo danh sách mới kiểu IEnumerable<SecondHand.Models.Domain.Order>
            var ordersList = getAnalyst.Select(item => new SecondHand.Models.Domain.Order
            {
                Id = item.Id,
                Total = item.Total
            }).ToList();
            var ordersByMonth = _context.Orders
            .GroupBy(x => x.OrderDate.Value.Month)
            .Select(group => new { 
                Month = group.Key, 
                TotalSales = group.Sum(x => x.Total), 
                OrderCount = group.Count() })
            .OrderBy(x => x.Month)
            .ToList();


            var currentMonth = DateTime.Now.Month;
            var salesByMonth = new List<decimal>();
            var ordersCountByMonth = new List<int>();
            var ListUser = _context.ApplicationUsers.ToList();


            for (int i = 1; i <= 12; i++)
            {
                foreach (var item in ordersByMonth)
                {
                    if(i == item.Month)
                    {
                        salesByMonth.Add(item.TotalSales);
                        ordersCountByMonth.Add(item.OrderCount);
                    }
                    else
                    {
                        salesByMonth.Add(0);
                        ordersCountByMonth.Add(0);
                    }
                }
            }
            AnalystVM analystVM = new AnalystVM()
            {
                OrderQuantity = ordersByMonth.Sum(x => x.OrderCount),
                UserQuantity = ListUser.Count(),
                TotalSales = ordersList.Sum(x => x.Total),
                TotalOrderPermonth = ordersCountByMonth,
                TotalSalesPerMonth = salesByMonth
            };
            return View(analystVM);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
