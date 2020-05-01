﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NToastNotify;
using QuickEmail.Data.IRepository;
using QuickEmail.Models;

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
            toastNotification.AddErrorToastMessage("Invalid Credential");
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

        public IActionResult UserRegister(User user)
        {
            var userDetail = new User();
            if (user != null)
            {
                userDetail = adminRepository.UserRegister(user);

                if (userDetail != null && userDetail.Status == "Success")
                {
                    HttpContext.Session.SetString("UserId", userDetail.UserId.ToString());
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

        #region

        public IActionResult VerifyUser(string verificationCode,Guid userId)
        {
           
            return View();
        }

        #endregion
    }
}