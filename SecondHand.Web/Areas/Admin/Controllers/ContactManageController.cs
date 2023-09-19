using Microsoft.AspNetCore.Mvc;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/ho-tro-khach-hang/[action]/{id?}")]
    public class ContactManageController : Controller
    {
        private readonly S2HandDbContext _context;

        public ContactManageController(S2HandDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Contact> contacts = _context.Contacts.ToList();
            return View(contacts);
        }
        public ActionResult Create()
        {
            Contact contact = new Contact();
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }
        public ActionResult Update(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, Contact updatedContact)
        {
            if (id != updatedContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingContact = _context.Contacts.Find(id);
                if (existingContact == null)
                {
                    return NotFound();
                }

                existingContact.FullName = updatedContact.FullName;
                existingContact.Email = updatedContact.Email;
                existingContact.Phone = updatedContact.Phone;
                existingContact.Message = updatedContact.Message;

                _context.Update(existingContact);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedContact);

        }
        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Brand collection)
        {
            var contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
