using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RemoteLocker.Common.Library.Encryption
{
    /// <summary>
    /// Ecryption using Md5/Sha1 algorithm
    /// </summary>
    public class Md5Sha1Encrypt
    {
        /// <summary>
        /// Hashing a plain text by Md5 algorithm
        /// </summary>
        /// <param name="InputString">Plain text for hashing</param>
        /// <returns></returns>
        public static String SHA1Hashing(String InputString)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(Encoding.Default.GetBytes(InputString));

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Hashing a plain text by Sha1 algorithm
        /// </summary>
        /// <param name="InputString">Plain text for hashing</param>
        /// <returns></returns>
        public static String MD5Hashing(String InputString)
        {
            MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(Encoding.Default.GetBytes(InputString));

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
