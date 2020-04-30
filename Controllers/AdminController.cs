using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using QuickEmail.Data.IRepository;
using QuickEmail.Models;

namespace QuickEmail.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepository;
        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public IActionResult SignIn()
        {

            var sessionEmail = HttpContext.Session.GetString("Email");

            if(!string.IsNullOrEmpty(sessionEmail))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        #region Sign In Post
        [HttpPost]

        public IActionResult UserSignIn(User user)
        {
            bool isValidUser = false;
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                isValidUser = adminRepository.GetUserDetails(user);

                if (isValidUser)
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return RedirectToAction("SignIn","Admin");

        }
        #endregion


        #region SignOut
        /// <summary>
        /// SignOut
        /// </summary>
        /// <returns></returns>

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Admin");
        }
        #endregion
    }
}