﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HistoryController : Controller
    {
        S2HandDbContext _context = new S2HandDbContext();
        public IActionResult Index(string user_id)
        {
            List<Order> history = _context.Orders
                .Include(x=>x.User)
                .Where(x=>x.User.Id == user_id)
                .ToList();  
            return View(history);
        }
    }
}
