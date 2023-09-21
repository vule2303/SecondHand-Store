using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/ma-khuyen-mai/[action]/{id?}")]
    public class PromotionsController : Controller
    {
        private readonly S2HandDbContext _context;

        public PromotionsController(S2HandDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Promotions
        public async Task<IActionResult> Index()
        {
              return _context.Promotions != null ? 
                          View(await _context.Promotions.ToListAsync()) :
                          Problem("Entity set 'S2HandDbContext.Promotions'  is null.");
        }

        // GET: Admin/Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // GET: Admin/Promotions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,DiscountType,DiscountValue,MiniumOrderAmount,Created,Modifield,IsActive,StartDate,EndDate")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotion);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tạo mới thành công";

                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Tạo mới không thành công";
            return View(promotion);
        }

        // GET: Admin/Promotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        // POST: Admin/Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,DiscountType,DiscountValue,MiniumOrderAmount,Created,Modifield,IsActive,StartDate,EndDate")] Promotion promotion)
        {
            if (id != promotion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotion);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Cập nhật không thành công";
            return View(promotion);
        }

        // GET: Admin/Promotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // POST: Admin/Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promotions == null)
            {
                return Problem("Entity set 'S2HandDbContext.Promotions'  is null.");
            }
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xoá thành công";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Xoá không thành công";
            return NotFound();
        }

        private bool PromotionExists(int id)
        {
          return (_context.Promotions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
