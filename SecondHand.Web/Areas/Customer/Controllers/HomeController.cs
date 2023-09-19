using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using SecondHand.DataAccess.Data;
using SecondHand.Models;
using SecondHand.Models.Domain;
using System.Diagnostics;
using System.Linq;

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

        public IActionResult Index()
        {
            var listProduct = _context.Products.Take(10).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}