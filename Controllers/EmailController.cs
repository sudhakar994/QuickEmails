using System;
using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using QuickEmail.Data.IRepository;
using QuickEmail.Models;
using QuickEmail.Utility;

namespace QuickEmail.Controllers
{
    [UserAuthenticationFilter]
    public class EmailController : Controller
    {
        private readonly IEmailRepository emailRepository;
        private readonly IToastNotification toastNotification;
        public EmailController(IEmailRepository emailRepository, IToastNotification toastNotification)
        {
            this.emailRepository = emailRepository;
            this.toastNotification = toastNotification;
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
            var contactDetails = new ContactDetails();
            Guid userId = HttpContext.Session.GetString("UserId") != null ? Guid.Parse(HttpContext.Session.GetString("UserId").ToString()) : Guid.Empty;
            if (userId != Guid.Empty)
            {
                contactDetails = emailRepository.GetContactDetails(userId);
            }
            return View(contactDetails);
        }

        #endregion

        #region _AddContacts
        /// <summary>
        /// _AddContacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult _AddContacts(long? contactId)
        {
            var contacts = new Contacts();

            if (contactId > 0 && contactId != null)
            {
                contacts = emailRepository.GetContacts(contactId);
            }

            return PartialView(contacts);
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
            bool isSavedContacts = false;
            Guid userId = HttpContext.Session.GetString("UserId") != null ? Guid.Parse(HttpContext.Session.GetString("UserId").ToString()) : Guid.Empty;
            if (userId != Guid.Empty)
            {
                contacts.UserId = userId;
                isSavedContacts = emailRepository.SaveContacts(contacts);

            }

            return Json(isSavedContacts);
        }

        #endregion


    }
}