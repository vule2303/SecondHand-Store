﻿using Microsoft.AspNetCore.Mvc;

namespace MVC_Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/s2Handstore/trang-chu/[action]/{id?}")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}