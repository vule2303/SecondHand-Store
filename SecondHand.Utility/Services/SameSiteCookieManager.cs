//using Microsoft.Owin;
//using Microsoft.Owin.Infrastructure;
//using CookieOptions = Microsoft.Owin.CookieOptions;


//namespace MVC_Core.Services
//{
//    public class SameSiteCookieManager : ICookieManager
//    {
//        private readonly ICookieManager _innerManager;

//        public SameSiteCookieManager() : this(new CookieManager())
//        {
//        }

//        public SameSiteCookieManager(ICookieManager innerManager)
//        {
//            _innerManager = innerManager;
//        }

//        public void AppendResponseCookie(IOwinContext context, string key, string value,
//                                         CookieOptions options)
//        {
//            CheckSameSite(context, options);
//            _innerManager.AppendResponseCookie(context, key, value, options);
//        }

//        public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
//        {
//            CheckSameSite(context, options);
//            _innerManager.DeleteCookie(context, key, options);
//        }

//        public string GetRequestCookie(IOwinContext context, string key)
//        {
//            return _innerManager.GetRequestCookie(context, key);
//        }

//        private void CheckSameSite(IOwinContext context, CookieOptions options)
//        {
//            if (options.SameSite == Microsoft.Owin.SameSiteMode.None && DisallowsSameSiteNone(context))
//            {
//                options.SameSite = null;
//            }
//        }

//        public static bool DisallowsSameSiteNone(IOwinContext context)
//        {
//            // TODO: Use your User Agent library of choice here.
//            var userAgent = context.Request.Headers["User-Agent"];
//            if (string.IsNullOrEmpty(userAgent))
//            {
//                return false;
//            }
//            return userAgent.Contains("BrokenUserAgent") ||
//                   userAgent.Contains("BrokenUserAgent2");
//        }

    
//    }
//}
