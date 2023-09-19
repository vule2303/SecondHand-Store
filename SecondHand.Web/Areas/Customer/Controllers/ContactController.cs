using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        private readonly S2HandDbContext _context;

        public ContactController(S2HandDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
        public ActionResult Create()
        {

            return View();
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
