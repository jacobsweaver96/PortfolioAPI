using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Xml;

namespace PortfolioAPI.Util
{
    public static class SecurityUtil
    {
        public static string GenerateSalt(int byteCount)
        {
            var saltArray = new byte[64];

            var cryptoProvider = new RNGCryptoServiceProvider();
            cryptoProvider.GetNonZeroBytes(saltArray);

            return Convert.ToBase64String(saltArray);
        }

        public static string GetSaltedPassword(string password, string salt)
        {
            var saltArray = Convert.FromBase64String(salt);
            var deriveBytes = new PasswordDeriveBytes(password, saltArray);
            var tdes = new TripleDESCryptoServiceProvider();

            try
            {
                tdes.Key = deriveBytes.CryptDeriveKey("TripleDES", "SHA512", 192, tdes.IV);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            return Convert.ToBase64String(tdes.Key);
        }

        public static bool VerifyPassword(string password, string salt, string passwordKey)
        {
            return GetSaltedPassword(password, salt) == passwordKey;
        }
    }
}