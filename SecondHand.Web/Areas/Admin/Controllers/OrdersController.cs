using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/don-dat-hang/[action]/{id?}")]
    [Authorize(Roles = "Admin")]

    public class OrdersController : Controller
    {
        private readonly S2HandDbContext _context;

        public OrdersController(S2HandDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int pg = 1)
        {
            var s2HandDbContext = await _context.Orders
            .Include(o => o.Promotion)
            .Include(o => o.User)
            .Where(o => o.Promotion != null && o.User != null)
            .OrderByDescending(d => d.OrderDate)// Kiểm tra xem các thuộc tính liên quan có giá trị null hay không
            .ToListAsync();
            //return View(s2HandDbContext);
            

            const int pageSize = 7;
            if (pg < 1)
                pg = 1;

            int recsCount = s2HandDbContext.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = s2HandDbContext.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
             
            //return View(cateList);

            return View(s2HandDbContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }
            var listOdered = _context.Orders
               .Include(d => d.OrderDetails)
               .ThenInclude(p => p.Product)
               .ThenInclude(x => x.productGallery)
               .Include(u => u.User)
               .FirstOrDefault(x => x.Id == id);
            if (listOdered == null)
            {
                return NotFound();
            }

            return View(listOdered);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Code");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PromotionId,PaymentStatus,PaymentMethod,OrderStatus,Subtotal,Disscount,Total,Note,Created,IsCancelled")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tạo mới thành công";
                return RedirectToAction("Index");
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Code", order.PromotionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            TempData["Error"] = "Tạo mới không thành công";
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Code", order.PromotionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PromotionId,PaymentStatus,PaymentMethod,OrderStatus,Subtotal,Disscount,Total,Note,Created,IsCancelled")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Code", order.PromotionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            TempData["Error"] = "Cập nhật không thành công";
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Promotion)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'S2HandDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xoá thành công";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Xoá không thành công";
            return NotFound();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            IQueryable<Order> query = _context.Orders;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.OrderCode.Contains(searchTerm));
            }

            List<Order> searchResults = await query.ToListAsync(); // Lấy kết quả tìm kiếm
            ViewBag.SearchTerm = searchTerm;


            return View("Index", searchResults); // Truyền kết quả tìm kiếm vào trang Index
        }
    }
}
