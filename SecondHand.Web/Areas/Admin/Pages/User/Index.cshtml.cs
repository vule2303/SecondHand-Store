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

        public const int Items_per_page = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }
        public async Task OnGet()
        {
            //users = await _userManager.Users.OrderBy(r => r.UserName).ToListAsync();
            var qr = _userManager.Users.OrderBy(r => r.UserName);
            var totalUser = await _userManager.Users.CountAsync();

            countPages = (int)Math.Ceiling((double)totalUser / Items_per_page);

            if (currentPage < 1)
                currentPage = 1;

            if (currentPage > countPages)
                currentPage = countPages;

            var qr1 = qr.Skip((currentPage - 1) * Items_per_page)
                  .Take(Items_per_page);

            users = await qr1.ToListAsync();

           
        }

        public void OnPost() => RedirectToPage();

    }
}
