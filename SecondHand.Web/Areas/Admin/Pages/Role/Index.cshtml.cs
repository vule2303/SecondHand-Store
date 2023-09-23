using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC_Core.Areas.Admin.Pages.Role;
using SecondHand.DataAccess.Data;

namespace MVC_Core.Areas.Identity.Pages.Role
{
    [Area("Admin")]
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, S2HandDbContext context) : base(roleManager, context)
        {
        }

        public List<IdentityRole> roles { get; set; }  
        public async Task OnGet()
        {
          roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        public void OnPost() => RedirectToPage();

    }
}
