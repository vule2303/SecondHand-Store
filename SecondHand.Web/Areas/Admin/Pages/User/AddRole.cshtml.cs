using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.Migrations;
using SecondHand.Models.Domain;
using System.ComponentModel;

namespace MVC_Core.Areas.Admin.Pages.User
{
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        [TempData]
        public string StatusMessage { get; set; }

        public ApplicationUser user { get; set; }

        [BindProperty]
   
        public string[] RoleNames { get; set; }

        public SelectList allRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
            {
                return NotFound($"Không có user");
            }

            user = await _userManager.FindByIdAsync(roleid);

            if (user == null)
            {
                return NotFound($"Không thấy user, id = {roleid}.");
            }

            RoleNames = (await _userManager.GetRolesAsync(user)).ToArray<string>();





            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
            {
                return NotFound($"Không có user");
            }

            user = await _userManager.FindByIdAsync(roleid);

            if (user == null)
            {
                return NotFound($"Không thấy user, id = {roleid}.");
            }

            // RoleNames

            var OldRoleNames = (await _userManager.GetRolesAsync(user)).ToArray();
            var deleteRoles = OldRoleNames.Where(r => !RoleNames.Contains(r));
            var addRoles = RoleNames.Where(r => !OldRoleNames.Contains(r));
             int deleteRolesCount = deleteRoles.Count();
            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);
            if(deleteRolesCount != 0)
            {
                var resultDelete = await _userManager.RemoveFromRolesAsync(user, deleteRoles);
                if (!resultDelete.Succeeded)
                {
                    resultDelete.Errors.ToList().ForEach(error => {
                        ModelState.AddModelError(string.Empty, error.Description);
                    });
                    return Page();
                }
            }
            await _userManager.UpdateSecurityStampAsync(user);
            var resultAdd = await _userManager.AddToRolesAsync(user, addRoles);
            if (!resultAdd.Succeeded)
            {
                resultAdd.Errors.ToList().ForEach(error => {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }


            StatusMessage = $"Vừa cập nhật role cho user: {user.UserName}";

            return RedirectToPage("./Index");
        }
    }
}
