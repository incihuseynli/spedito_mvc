﻿using Microsoft.AspNetCore.Mvc;

namespace Spedito.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
