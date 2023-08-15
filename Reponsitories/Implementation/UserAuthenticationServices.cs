using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using MVC_Core.Context;
using MVC_Core.Models.Domain;
using MVC_Core.Models.DTO;
using MVC_Core.Reponsitories.Abstract;
using System.Security.Claims;

namespace MVC_Core.Reponsitories.Implementation
{
    public class UserAuthenticationServices : IUserAuthentication
    {
        private readonly S2HandDbContext _context;
        private readonly SignInManager<User> signinManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationServices(RoleManager<IdentityRole> roleManager, SignInManager<User> signinManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.signinManager = signinManager;
            this.userManager = userManager;
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.UserName);
            if(user ==null)
            {
                status.StatusCode = 0;
                status.Message = "Sai tài khoản hoặc mật khẩu";
                return status;
            }
            //we will match password
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Sai mật khẩu";
                return status;
            }

            var signInResult = await signinManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded) 
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Đăng nhập thành công";
                return status;
            }
            else if(signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "Tài khoản đã bị khoá";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Lỗi ẩn danh ";
                return status;
            }
        }

        public async Task LogoutAnsync()
        {
            await signinManager.SignOutAsync();
        }

        public async Task<Status> RegisterAnsync(RegisterModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Tài khoản đã tồn tại";
                return  status;
            }

            User user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Tạo tài khoản không thành công";
            }

            //Role management
            if(!await roleManager.RoleExistsAsync(model.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            if(await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "Tạo tài khoản thành công";
            return status;           
                
            
        }
    }
}
