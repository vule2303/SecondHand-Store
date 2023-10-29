using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecondHand.DataAccess.Data;
using SecondHand.DataAccess.Migrations;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MVC_Core.Areas.Admin.Pages.Role
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : RolePageModel
    {
        public DeleteModel(RoleManager<IdentityRole> roleManager, S2HandDbContext context) : base(roleManager, context)
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
        public async Task<IActionResult> OnGet(string roleid)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");

            var role = await _roleManager.FindByIdAsync(roleid);

            if (role == null)
            {
                
                return NotFound("Không tìm thấy role");

            }
            else
            {
                Input = new InputModel
                {
                    Name = role.Name
                };
                return Page();
            }         
        }
        public async Task<IActionResult> OnPostAsync(string roleid) 
        {

            if (roleid == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Không tìm thấy role");
        
            
            var result = await _roleManager.DeleteAsync(role);


            if (result.Succeeded)
            {
                TempData["Success"] = $"Xoá Role {role.Name} thành công";
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
