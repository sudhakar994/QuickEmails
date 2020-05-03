using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using QuickEmail.Data.IRepository;
using QuickEmail.Models;
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

        #region Contacts
        /// <summary>
        /// Contacts
        /// </summary>
        /// <returns></returns>

        public IActionResult Contacts()
        {

            return View();
        }

        #endregion

        #region _AddContacts
        /// <summary>
        /// _AddContacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _AddContacts()
        {
            return PartialView();
        }
        #endregion

        #region SaveContacts
        /// <summary>
        /// SaveContacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>

            [HttpPost]
        public JsonResult SaveContacts(Contacts contacts)
        {


            return Json(true);
        }

        #endregion


    }
}