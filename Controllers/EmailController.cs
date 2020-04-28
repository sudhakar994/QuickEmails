using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using QuickEmail.Data.IRepository;
using QuickEmail.Utility;

namespace QuickEmail.Controllers
{
    [UserAuthenticationFilter]
    public class EmailController : Controller
    {
        private readonly IEmailRepository emailRepository;
        public EmailController(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            var details = emailRepository.Test();
            ViewBag.Data = details;
            return View();
        }



    }
}