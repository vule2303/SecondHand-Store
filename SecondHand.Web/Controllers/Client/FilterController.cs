﻿using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Controllers.Client
{
    public class FilterController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Client/Filter/Filter.cshtml");
        }
    }
}