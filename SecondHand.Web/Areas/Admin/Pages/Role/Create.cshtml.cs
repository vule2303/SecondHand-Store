using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecondHand.DataAccess.Data;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core.Areas.Admin.Pages.Role
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, S2HandDbContext context) : base(roleManager, context)
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
        public InputModel Input { get; set;}

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Tạo mới không thành công";
                return Page();
            }
            var newRole = new IdentityRole(Input.Name);
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                TempData["Success"] = "Tạo mới thành công";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["Error"] = "Tạo mới không thành công";
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }
           
        }
    }
}
