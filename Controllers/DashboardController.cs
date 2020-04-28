﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickEmail.Utility;

namespace QuickEmail.Controllers
{
    [UserAuthenticationFilter]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}