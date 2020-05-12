using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEmail.Utility
{
    public static class SqlStringConstant
    {
        public const string GetUserDetail= "Select User_Id UserId, Email Email ,User_Name UserName, Password Password,Password_Salt PasswordSalt,Is_Verified IsVerified FROM em_Users WHERE Email=@Email AND  Is_Deleted=0";

        public const string GetEmailAddress = "SELECT Email Email FROM em_Users  WHERE Email=@Email AND Is_Deleted=0";
        public const string SaveUser = "INSERT INTO em_Users  ( User_Name, Email,Password,Password_Salt , Verification_Code) OUTPUT INSERTED.User_Id VALUES (@UserName,@Email,@Password,@PasswordSalt,@VerificationCode)";
        public const string GetVerifactionCodeByUserId = "Select User_Id UserId,Verification_Code VerificationCode,User_Name UserName ,Email Email From  em_Users Where User_Id=@UserId AND Is_Deleted=0";
        public const string UpdatedVerification = "UPDATE em_Users SET Is_Verified=1, Updated_Time_Stamp =GETDATE() where User_Id=@UserId AND Is_Deleted=0";
        public const string SaveContacts = "INSERT INTO em_Contacts (User_Id,Contact_Name ,Email,Phone,Country,User_Domain ) OUTPUT INSERTED.Contact_Id VALUES(@UserId,@ContactName,@ContactEmail,@ContactPhone,@Country,@UserDomain) ";
        public const string GetContactId = "SELECT Contact_Id FROM em_Contacts  WHERE Contact_Id = @ContactId AND Is_Deleted = 0";
        public const string UpdateContactsbByContactId = "Update em_Contacts SET Contact_Name=@ContactName,Email=@ContactEmail,Phone=@ContactPhone,Country=@Country,User_Domain=@UserDomain,Updated_Time_Stamp=GETDATE() WHERE Contact_Id=@ContactId AND  Is_Deleted=0";
        public const string GetContactDetails = "SELECT Contact_Id ContactId,User_Id UserId, Contact_Name ContactName,Email ContactEmail, Phone ContactPhone,Country Country, User_Domain UserDomain,Create_Time_Stamp CreateTimeStamp FROM em_Contacts WHERE User_Id=@UserId AND Is_Deleted=0";
        public const string GetContactsByContactId = "SELECT Contact_Id ContactId,User_Id UserId, Contact_Name ContactName,Email ContactEmail, Phone ContactPhone,Country Country, User_Domain UserDomain,Create_Time_Stamp CreateTimeStamp FROM em_Contacts WHERE Contact_Id=@ContactId AND Is_Deleted=0";
        public const string DeleteContacts = "UPDATE em_Contacts SET Is_Deleted=1,Updated_Time_Stamp =GETDATE() WHERE Contact_Id=@ContactId ";
    }
}
