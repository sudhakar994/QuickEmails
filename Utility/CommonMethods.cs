using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickEmail.Utility
{
    public class CommonMethods
    {

        public static string CreateSalt(int saltSize)
        {
            var buff = new byte[saltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buff);
            }
            return Convert.ToBase64String(buff);
        }
        public static string EncryptPassword(string pPassword, string pSalt)
        {
            var saltAndPwd = String.Concat(pPassword, pSalt);
            var hashedPwd = GetSwcSha1(saltAndPwd);
            return hashedPwd;
        }


        static string GetSwcSha1(string value)
        {
            var algorithm = SHA1.Create();
            var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sh1 = new StringBuilder();
            foreach (byte t in data)
            {
                sh1.Append(t.ToString("x2").ToUpperInvariant());
            }
            return sh1.ToString();
        }


    }
}
