using QuickEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Data.IRepository
{
  public  interface IAdminRepository
    {
        bool GetUserDetails(User user);
    }
}
