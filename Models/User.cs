using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Models
{
    public class User
    {
        public Guid UserId { get; set; }
       
        public string UserName { get; set; }
      
        public string Email { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }

        public string PasswordSalt { get; set; }
        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }
    }
}
