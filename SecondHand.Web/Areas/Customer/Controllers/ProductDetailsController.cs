using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("s2Handstore/chi-tiet/{id?}")]
    public class ProductDetailsController : Controller
    {
        private readonly S2HandDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public ProductDetailsController(S2HandDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
