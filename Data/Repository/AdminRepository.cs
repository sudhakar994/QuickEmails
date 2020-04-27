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
using Dapper;

namespace QuickEmail.Data.Repository
{
    public class AdminRepository : IAdminRepository
    {
       

        private readonly IConfiguration _config;

        public AdminRepository(IConfiguration config)
        {
            _config = config;
        }
        private IDbConnection quickEmaildbConnection => new SqlConnection(_config.GetConnectionString("QuickEmailNewDb"));
    
        private string GetAppSettings(string key)
        {
            var value = _config.GetSection("AppSettings:" + key)?.Value;
            if (value != null)
            {
                return value;
            }
            return string.Empty;
        }


        #region Get User Details
        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GetUserDetails(User user)
        {
            bool isValidUser = false;
            var userDetails = new User();

            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                using (var dbConnection = quickEmaildbConnection)
                {
                    userDetails = dbConnection.Query<User>(SqlStringConstatnt.GetUserDetail).SingleOrDefault();

                    if(userDetails != null)
                    {
                        if(userDetails.Email == user.Email && userDetails.Password == user.Password)
                        {
                            isValidUser = true;
                            
                        }
                    }
                }
            }
            return isValidUser;
        }

        #endregion

    }
}
