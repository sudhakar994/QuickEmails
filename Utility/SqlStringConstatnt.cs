using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Utility
{
    public static class SqlStringConstant
    {
        public const string GetUserDetail= "Select  email Email , password Password FROM loginuser ";

        public const string GetEmailAddress = "SELECT Email Email FROM em_Users  WHERE Email=@Email AND Is_Deleted=0";
        public const string SaveUser = "INSERT INTO em_Users  ( User_Name, Email,Password,Password_Salt , Verification_Code) OUTPUT INSERTED.User_Id VALUES (@UserName,@Email,@Password,@PasswordSalt,@VerificationCode)";
        public const string GetVerifactionCodeByUserId = "Select User_Id UserId,Verification_Code VerificationCode,User_Name UserName ,Email Email From  em_Users Where User_Id=@UserId AND Is_Deleted=0";
    }
}
