using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Controllers.Client
{
    public class HomeController1 : Controller
    {
        public string Index()
        {
            return "This is my default action...";
        }

        public string Welcome()
        {
            return "This is the Welcome action method.... ";
        }
    }
}
