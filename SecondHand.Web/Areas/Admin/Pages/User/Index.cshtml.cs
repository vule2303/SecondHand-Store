using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC_Core.Areas.Admin.Pages.Role;
using SecondHand.DataAccess.Data;
using SecondHand.Models.Domain;

namespace MVC_Core.Areas.Admin.Pages.User
{
    [Area("Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<ApplicationUser> users { get; set; }
        public async Task<IActionResult> OnGet()
        {
            users = await _userManager.Users.OrderBy(r => r.UserName).ToListAsync();
            return Page();
        }

        public void OnPost() => RedirectToPage();

    }
}
