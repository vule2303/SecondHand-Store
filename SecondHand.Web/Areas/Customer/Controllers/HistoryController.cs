using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.ViewModels;
using SecondHand.Utility.Services;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HistoryController : Controller
    {
        S2HandDbContext _context = new S2HandDbContext();
        public IActionResult Index()
        {

            var claimsIdenity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdenity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                List<Order> history = _context.Orders
               .Include(x => x.User)
               .Where(x => x.User.Id == claim.Value)
               .ToList();
                return View(history);

            }

            return RedirectToAction("Error", "Home");


        }
    }
}
