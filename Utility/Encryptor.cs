using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Utility
{
    public static class Encryptor
    {
        private enum EnumTokenValidity : byte
        {
            Valid = 1,
            Expired = 2,
            NotMatching = 3
        }

        /// <summary> s
        /// Get the session from HttpContext.Current, if that is null try to get it from the Request properties.
        /// </summary>
        /// <returns></returns>
        public static HttpContextWrapper GetHttpContextWrapper(HttpRequestMessage request)
        {
            HttpContextWrapper httpContextWrapper = null;
            if (HttpContext.Current != null)
                httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            else if (request.Properties.ContainsKey("MS_HttpContext"))
                httpContextWrapper = (HttpContextWrapper)request.Properties["MS_HttpContext"];
            //httpContextWrapper.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            return httpContextWrapper;
        }

        /// <summary>
        /// Generates time-based tokens that expire after provided time period.
        /// </summary>
        /// <param name="expiryTimeInMinutes">Time in minutes after which the token expires.</param>
        /// <param name="uniqueDeviceId">Unique device identification associated with the ticket.</param>
        /// <returns>Random token.</returns>
        public static string GenerateExpiryToken(int expiryTimeInMinutes, string uniqueDeviceId)
        {
            var formsTicket = new FormsAuthenticationTicket(
                   1, uniqueDeviceId,
                   DateTime.Now,
                   DateTime.Now.AddMinutes(expiryTimeInMinutes),
                   false,
                   uniqueDeviceId
                   );

            var encryptedTicket = FormsAuthentication.Encrypt(formsTicket);
            return encryptedTicket;
        }

        /// <summary>
        /// Verify the validity of returned time-based token.
        /// Return value 1 means token is valid. 2 means token is valid but has expired. 3 means token didn't match.
        /// </summary>
        /// <param name="encryptedTicket">The token that the client returned.</param>
        /// <param name="expecteduniqueDeviceId">The attached Unique device identification that was passed within the token.</param>
        /// <returns>Signal that verifies whether the token was validated or failed validation.</returns>
        public static byte VerifyToken(string encryptedTicket, string expecteduniqueDeviceId)
        {
            try
            {
                if (!string.IsNullOrEmpty(encryptedTicket))
                {
                    var formsTicket = FormsAuthentication.Decrypt(encryptedTicket);

                    if (formsTicket != null)
                    {
                        //var requestAfter = Convert.ToInt32((DateTime.Now - formsTicket.IssueDate).TotalSeconds);
                        //if (requestAfter < 1) //To reduce DOS attack. Take from config.
                        //    return 2;

                        // Verify that the Unique device identification in the ticket matches one that was sent with the request
                        if (formsTicket.Name.Equals(expecteduniqueDeviceId))
                            return (byte)(DateTime.Now <= formsTicket.Expiration ? EnumTokenValidity.Valid : EnumTokenValidity.Expired);
                    }
                }
                return (byte)EnumTokenValidity.NotMatching;
            }
            catch (Exception)
            {
                return (byte)EnumTokenValidity.NotMatching;
            }
        }

        /// <summary>
        /// Create a salt key for hashing algorithm.
        /// </summary>
        /// <param name="size">Requested length.</param>
        /// <returns></returns>
        public static string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Creates a hash for the password.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <param name="saltkey">The salt for the hashing.</param>
        /// <param name="passwordFormat">Password format of FormsAuthPasswordFormat.SHA1 enum type.</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            var saltAndPassword = String.Concat(password, saltkey);
            var hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            return hashedPassword;
        }

        public static string EncryptHelper(string plainText)
        {
            const string key = "jmdn4323";
            byte[] iv = { 55, 34, 87, 64, 87, 195, 54, 21 };
            var encryptKey = Encoding.UTF8.GetBytes(key);
            var des = new DESCryptoServiceProvider();
            var inputByte = Encoding.UTF8.GetBytes(plainText);
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, des.CreateEncryptor(encryptKey, iv), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return HttpServerUtility.UrlTokenEncode(mStream.ToArray());
        }
    }
}
