using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Controllers.Client
{
    public class ContactController : Controller
    {
        private readonly S2HandDbContext _context;

        public ContactController(S2HandDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Contact> contact = _context.Contacts.ToList();
            return View(contact);
        }
        public ActionResult Create()
        {
            var model = new Contact();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                return RedirectToAction("Announce");
            }
            return View(contact);

        }
        public ActionResult Announce()
        {
            return View("/Views/Contact/Announce.cshtml");
        }
    }
}
