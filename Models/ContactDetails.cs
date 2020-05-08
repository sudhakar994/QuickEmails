using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Models
{
    public class ContactDetails
    {
        public List<Contacts> Contacts { get; set; }
    }

    public class Contacts
    {
        public Guid UserId { get; set; }

        public long ContactId { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string Country { get; set; }

        public string UserDomain { get; set; }
        public DateTime CreateTimeStamp { get; set; }
    }
}
