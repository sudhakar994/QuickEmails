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
        private IDbConnection quickEmaildbConnection => new SqlConnection(_config.GetConnectionString("QuickEmailDb"));

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
                    userDetails = dbConnection.Query<User>(SqlStringConstant.GetUserDetail).SingleOrDefault();

                    if (userDetails != null)
                    {
                        if (userDetails.Email == user.Email && userDetails.Password == user.Password)
                        {
                            isValidUser = true;

                        }
                    }
                }
            }
            return isValidUser;
        }

        #endregion

        #region UserRegister
        /// <summary>
        /// UserRegister
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UserRegister(User user)
        {
            var userDetail = new User();
            if (user != null)
            {
                using (var dbConnection = quickEmaildbConnection)
                {


                    var emailAddress = dbConnection.Query<string>(SqlStringConstant.GetEmailAddress, user).SingleOrDefault();

                    if (string.IsNullOrWhiteSpace(emailAddress))
                    {
                        #region Create Password Salt Key and Encryption

                        user.PasswordSalt = CommonMethods.CreateSalt(8);
                        user.Password = CommonMethods.EncryptPassword(user.Password, user.PasswordSalt);

                        #endregion

                        Random generator = new Random();
                        user.VerificationCode = generator.Next(0, 999999).ToString("D6");

                        user.UserId = dbConnection.Query<Guid>(SqlStringConstant.SaveUser, user).SingleOrDefault();

                        if (user.UserId != Guid.Empty)
                        {
                            user.Status = "Success";
                        }
                        else
                        {
                            user.Status = "Failure";
                        }
                    }
                    else
                    {
                        userDetail.Status = "Email Already register with us!";
                    }

                    userDetail = user;





                }
            }
            return userDetail;
        }

        #region VerifyUser
        /// <summary>
        /// VerifyUser
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <returns></returns>

        public bool VerifyUser(string verificationCode,Guid userId)
        {
            bool isValidVerificationCode = false;
            if (!string.IsNullOrWhiteSpace(verificationCode))
            {
                using (var dbConnection = quickEmaildbConnection)
                {
                    var verifcationCode = dbConnection.Query<string>(SqlStringConstant.GetVerifactionCodeByUserId,new {UserId= userId }).SingleOrDefault();

                    if(verifcationCode == verificationCode)
                    {
                        isValidVerificationCode = true;
                    }
                }
            }
            return isValidVerificationCode;
        }

        #endregion

        #endregion

    }
}
