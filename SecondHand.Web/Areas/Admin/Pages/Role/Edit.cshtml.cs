using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecondHand.DataAccess.Data;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core.Areas.Admin.Pages.Role
{
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, S2HandDbContext context) : base(roleManager, context)
        {
        }

        public class InputModel
        {
            [Display(Name = "Tên của role")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 4, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự")]
            public string Name { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public IdentityRole role { get; set; }

        public async Task<IActionResult> OnGetAsync(string roleId)
        {
            if (roleId == null) return NotFound("Không tìm thấy role");

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                Input = new InputModel
                {
                    Name = role.Name
                };
                return Page();

            }


            return NotFound("Không tìm thấy role");

        }
        public async Task<IActionResult> OnPostAsync(string roleId)
        {
            if (roleId == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return NotFound("Không tìm thấy role");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Cập nhật Role không thành công";
                return Page();
            }

            role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                TempData["Success"] = "Cập nhật Role thành công";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["Error"] = "Cập nhật Role không thành công";
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }

        }
    }
}