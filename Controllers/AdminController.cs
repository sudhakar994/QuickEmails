using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NToastNotify;
using QuickEmail.Data.IRepository;
using QuickEmail.Models;
using QuickEmail.Utility;

namespace QuickEmail.Controllers
{
  
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepository;
        private readonly IToastNotification toastNotification;
        public AdminController(IAdminRepository adminRepository, IToastNotification toastNotification)
        {
            this.adminRepository = adminRepository;
            this.toastNotification = toastNotification;
        }

        public IActionResult SignIn()
        {


            var sessionEmail = HttpContext.Session.GetString("Email");

            if (!string.IsNullOrEmpty(sessionEmail))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        #region Sign In Post
        [HttpPost]

        public IActionResult UserSignIn(User user)
        {
            var userDetails = new User();
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                userDetails = adminRepository.GetUserDetails(user);

                if (userDetails != null  && userDetails.Status == "ValidUser")
                {
                    HttpContext.Session.SetString("Email", userDetails.Email);
                    HttpContext.Session.SetString("UserName", userDetails.UserName);
                    HttpContext.Session.SetString("UserId", userDetails.UserId.ToString());
                    HttpContext.Session.SetString("VerifictionSuccess", "VerificationSuccess");
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            toastNotification.AddErrorToastMessage("Invalid credential");
            return RedirectToAction("SignIn", "Admin");

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


        #region Register
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public IActionResult Register()
        {
            return View();
        }

        #endregion


        #region User Register
        /// <summary>
        /// UserRegister
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UserRegister(User user)
        {
            var userDetail = new User();
            if (user != null)
            {
                userDetail = adminRepository.UserRegister(user);

                if (userDetail != null && userDetail.Status == "Success")
                {
                    HttpContext.Session.SetString("UserId", userDetail.UserId.ToString());
                    HttpContext.Session.SetString("Email", userDetail.Email);
                    return RedirectToAction("VerifyUser", "Admin");
                }
                else
                {
                    toastNotification.AddErrorToastMessage("Email Already Registered!");
                    return RedirectToAction("Register", "Admin");
                }
            }
            return View();
        }

        #endregion

        #region Verify User
        /// <summary>
        /// VerifyUser
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        public IActionResult VerifyUser()
        {
        

            return View();
        }

        #endregion


        #region Validate Verification Code
        /// <summary>
        /// ValidateVerificationCode
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ValidateVerificationCode(string verificationCode)
        {
            var verifyUserDetail = new User();
            Guid userId = HttpContext.Session.GetString("UserId") != null ? Guid.Parse(HttpContext.Session.GetString("UserId").ToString()) : Guid.Empty;

            if (!string.IsNullOrEmpty(verificationCode) && userId != Guid.Empty)
            {
                verifyUserDetail = adminRepository.VerifyUser(verificationCode, userId);
                if (verifyUserDetail != null && verifyUserDetail.Status == "ValidUser")
                {
                    HttpContext.Session.SetString("Email", verifyUserDetail.Email);
                    HttpContext.Session.SetString("UserName", verifyUserDetail.UserName);
                    HttpContext.Session.SetString("VerifictionSuccess", "VerificationSuccess");
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (verifyUserDetail != null && verifyUserDetail.Status == "InvalidUser")
                {
                    toastNotification.AddErrorToastMessage("Invalid Verification Code!");
                    return RedirectToAction("VerifyUser", "Admin");
                }
            }
            return View();
        }

        #endregion
    }
}