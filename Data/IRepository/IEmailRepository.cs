using QuickEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Data.IRepository
{
    public interface IEmailRepository
    {
        string Test();
        bool SaveContacts(Contacts contacts);
        ContactDetails GetContactDetails(Guid userId);

        Contacts GetContacts(long? contactId);
        bool DeleteContact(long contactId);
    }
}
