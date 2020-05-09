using Dapper;
using Microsoft.Extensions.Configuration;
using QuickEmail.Data.IRepository;
using QuickEmail.Models;
using QuickEmail.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Data.Repository
{

    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;

        public EmailRepository(IConfiguration config)
        {
            _config = config;
        }
        private IDbConnection quickEmaildbConnection => new SqlConnection(_config.GetConnectionString("QuickEmailDb"));

        public string Test()
        {
            return "Test project";
        }

        #region Save Contacts
        /// <summary>
        /// SaveContacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>

        public bool SaveContacts(Contacts contacts)
        {
            bool isSavedContacts = false;

            if (contacts != null && contacts.UserId != Guid.Empty)
            {
                using (var dbConnection = quickEmaildbConnection)
                {
                    var contactId = dbConnection.Query<long>(SqlStringConstant.GetContactId, contacts).SingleOrDefault();
                    if (contactId > 0)
                    {
                        dbConnection.Execute(SqlStringConstant.UpdateContactsbByContactId, contacts);
                        isSavedContacts = true;
                    }
                    else
                    {
                        var insertedContactId = dbConnection.Query<long>(SqlStringConstant.SaveContacts, contacts).SingleOrDefault();

                        if (insertedContactId > 0)
                        {
                            isSavedContacts = true;
                        }
                    }

                }
            }

            return isSavedContacts;

        }
        #endregion


        #region Get Contact Details
        /// <summary>
        /// GetContactDetails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ContactDetails GetContactDetails(Guid userId)
        {
            var contactDetails = new ContactDetails { Contacts = new List<Contacts>() };
            if (userId != Guid.Empty)
            {
                using (var dbConnection = quickEmaildbConnection)
                {
                    contactDetails.Contacts = dbConnection.Query<Contacts>(SqlStringConstant.GetContactDetails, new { UserId = userId }).ToList();
                }
            }
            return contactDetails;

        }



        #endregion


        #region

        public Contacts GetContacts(long? contactId)
        {
            var contacts = new Contacts();
            {
                using (var dbConnection = quickEmaildbConnection)
                {
                    contacts = dbConnection.Query<Contacts>(SqlStringConstant.GetContactsByContactId, new { ContactId = contactId }).SingleOrDefault();
                }
            }
            return contacts;
        }

        #endregion
    }
}
