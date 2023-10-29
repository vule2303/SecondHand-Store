using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC_Core.Areas.Admin.Pages.Role;
using SecondHand.DataAccess.Data;
using SecondHand.DataAccess.Migrations;
using SecondHand.Models.Domain;
using System.Data;

namespace MVC_Core.Areas.Admin.Pages.User
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public class UserAndRole : ApplicationUser
        {
            public string RoleNames { get; set; }
        }
        public List<UserAndRole> users { set; get; }

        public const int ITEMS_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }

        public int totalUsers { get; set; }
        public async Task OnGet()
        {
            // users =  await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();
            var qr = _userManager.Users.OrderBy(u => u.UserName);

            totalUsers = await qr.CountAsync();
            countPages = (int)Math.Ceiling((double)totalUsers / ITEMS_PER_PAGE);

            if (currentPage < 1)
                currentPage = 1;
            if (currentPage > countPages)
                currentPage = countPages;

            var qr1 = qr.Skip((currentPage - 1) * ITEMS_PER_PAGE)
                        .Take(ITEMS_PER_PAGE)
                        .Select(u => new UserAndRole()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                        });

            users = await qr1.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }

        }

        public void OnPost() => RedirectToPage();

    }
}
