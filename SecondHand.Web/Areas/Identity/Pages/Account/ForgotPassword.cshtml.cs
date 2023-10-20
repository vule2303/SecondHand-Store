// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using SecondHand.Models.Domain;

namespace SecondHand.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPassword");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity",Input.Email,code },
                protocol: Request.Scheme);

                var PathToFile = _hostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Templates" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates" + Path.DirectorySeparatorChar.ToString() + "confirm_Forgot_Password.html";
                var subject = "Đặt lại mật khẩu";
                string htmlBody = "";
                using (StreamReader streamReader = System.IO.File.OpenText(PathToFile))
                {
                    htmlBody = streamReader.ReadToEnd();
                }
                //get time Vetnam
                CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
                DateTime now = DateTime.Now;
                var getTime = String.Format("{0}, {1:dd MM yyyy}", now.ToString("dddd", vietnameseCulture), now);



                string Message = $"Có vẻ bạn đã quên mật khẩu đăng nhập tại S2HandStore. Để đặt lại mật khẩu hãy ấn vào nút đặt lại mật khẩu";
                try
                {
                    string messageBody = string.Format(htmlBody, subject, getTime, user.FirstName + user.LastName, Message, callbackUrl);
                    // Tiếp tục xử lý messageBody và gửi email
                    await _emailSender.SendEmailAsync(Input.Email, subject, messageBody);
                }
                catch (FormatException ex)
                {
                    // Xử lý lỗi định dạng cụ thể
                    // In ra thông báo lỗi hoặc thực hiện các hành động cần thiết để xử lý lỗi
                    Console.WriteLine("Lỗi định dạng: " + ex.Message);
                }

                

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
