using QuickEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Data.IRepository
{
  public  interface IAdminRepository
    {
        User GetUserDetails(User user);
        User UserRegister(User user);
        User VerifyUser(string verificationCode, Guid userId);
    }
}
