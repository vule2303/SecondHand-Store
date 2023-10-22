
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Crmf;
using SecondHand.DataAccess.Data;
using SecondHand.DataAccess.Migrations;
using SecondHand.Models.Domain;
using SecondHand.Models.ViewModels;
using System.Security.Claims;

namespace MVC_Core.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PersonalController : Controller
    {
        public readonly S2HandDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly SignInManager<ApplicationUser> _signInManager;
        public PersonalController(S2HandDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [BindProperty]
        public ChangePasswordVM ChangePasswordVM { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChangePassWord()
        {         
            ChangePasswordVM = new ChangePasswordVM();                   
            return View(ChangePasswordVM);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var getUser = _context.ApplicationUsers.Where(x => x.Id == claim.Value).FirstOrDefault();
            var CheckPassword = await _signInManager.PasswordSignInAsync(getUser.Email, ChangePasswordVM.OldPassword, false, lockoutOnFailure: false);
            if (CheckPassword.Succeeded)
            {
                var addPasswordResult = await _userManager.ChangePasswordAsync(getUser, ChangePasswordVM.OldPassword, ChangePasswordVM.NewPassword);
                    if (!addPasswordResult.Succeeded)
                    { 
                        foreach (var error in addPasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return RedirectToAction("Error","Home");
                    }
               
                ViewBag.ConfirmChangePassword = true;
                return View();
            }
            ViewBag.ConfirmChangePassword = false;
            return View();
        }
    }
}
