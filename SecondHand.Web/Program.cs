
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Facebook;
using SecondHand.DataAccess.Data;
using SecondHand.Utility.Services;
using SecondHand.Models.Domain;
using Microsoft.AspNetCore.Builder;

namespace SecondHand
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            //add mailSender
            builder.Services.AddOptions();
            var mailSettings = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailSettings);
            builder.Services.AddSingleton<IEmailSender, SendMailService>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            //Add DbContext
            builder.Services.AddDbContext<S2HandDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("S2HandStore")));






            //Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<S2HandDbContext>()
                .AddDefaultTokenProviders();

            // builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedEmail = false);



            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
               // options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
               // options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = false; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;


            });

            //Add Authenticate Services
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                IConfiguration googleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                //Config Client Id and ClientSecret for access API google
                options.ClientId = googleAuthSection["ClientId"]; 
                options.ClientSecret = googleAuthSection["ClientSecret"];
                // config URL Callback from Goole(Base is /signin-google)
                options.CallbackPath = "/dang-nhap-tu-google";


            }).AddFacebook(facebookOptions => {
                // Đọc cấu hình
                IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
                facebookOptions.AppId = facebookAuthNSection["AppId"];
                facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
                // Thiết lập đường dẫn Facebook chuyển hướng đến
                facebookOptions.CallbackPath = "/dang-nhap-tu-facebook";
            });
            //sign for Interface

            //sign samesite
            void CheckSameSite(HttpContext httpContext, CookieOptions options)
            {
                if (options.SameSite == SameSiteMode.None)
                {
                    var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                    // TODO: Use your User Agent library of choice here.
                  
                    
                        // For .NET Core < 3.1 set SameSite = (SameSiteMode)(-1)
                        options.SameSite = SameSiteMode.Unspecified;
                    
                }
            }
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); 
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();        
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");       
            app.MapRazorPages();
           
            app.Run();
        }
    }
}