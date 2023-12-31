﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.Extensions.Logging;
using SecondHand.Models.Domain;
using SecondHand.Utility;

namespace SecondHand.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _hostEnvironment = hostEnvironment; 
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        ///  [TempData]
        public string StatusMessage { get; set; }
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Phải nhập {0} ")]
            [DataType(DataType.Text)]
            [Display(Name = "Họ")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Phải nhập {0} ")]
            [DataType(DataType.Text)]
            [Display(Name = "Tên")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Phải nhập {0} ")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required (ErrorMessage ="Phải nhập {0} ")]
            [DataType(DataType.Text)]
            [Display(Name = "tên tài khoản")]
            public string UserName { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Phải nhập {0} ")]
            [StringLength(100, ErrorMessage = "{0} phải từ {2} đến {1} ký tự", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }          
 
            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận password")]
            [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không đúng")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "Phải nhập {0} ")]
            [StringLength(10, ErrorMessage = "{0} phải nhập {1} chữ số")]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Số điện thoại")]
            public string Phone { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/");
            }
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.FirstName = Input.FirstName; 
                user.LastName = Input.LastName;
                user.Created = DateTime.Now;
                Input.UserName = Input.Email;
                await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                await _userManager.SetPhoneNumberAsync(user, Input.Phone);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, SD.Role_User_Cust);
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    var PathToFile = _hostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Templates" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates" + Path.DirectorySeparatorChar.ToString() + "confirm_Account_Registration.html";
                    var subject = "Xác nhận Email đăng ký";
                    string htmlBody = "";
                    using (StreamReader streamReader = System.IO.File.OpenText(PathToFile))
                    {
                        htmlBody = streamReader.ReadToEnd();
                    }
                    //get time Vetnam
                    CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
                    DateTime now = DateTime.Now;
                    var getTime = String.Format("{0}, {1:dd MM yyyy}", now.ToString("dddd", vietnameseCulture),now);



                   string Message = $"Bạn nhận được email này bởi vì bạn đã đăng ký tài khoản mua sắm tại S2HandStore\r\nHãy ấn vào nút Xác thực để tiến hành xác thực Email của bạn!";
                    try
                    {
                        string messageBody = string.Format(htmlBody, subject, getTime,user.Email, user.FirstName + user.LastName, Message, callbackUrl);
                        // Tiếp tục xử lý messageBody và gửi email
                        await _emailSender.SendEmailAsync(Input.Email, subject, messageBody);
                    }
                    catch (FormatException ex)
                    {
                        // Xử lý lỗi định dạng cụ thể
                        // In ra thông báo lỗi hoặc thực hiện các hành động cần thiết để xử lý lỗi
                        Console.WriteLine("Lỗi định dạng: " + ex.Message);
                    }


                   


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
