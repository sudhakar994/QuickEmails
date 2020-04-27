using QuickEmail.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Data.Repository
{
   
    public class EmailRepository : IEmailRepository
    {
        public string Test()
        {
            return "Test project";
        }
    }
}
