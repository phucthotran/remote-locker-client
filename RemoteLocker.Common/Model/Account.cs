using System;

namespace RemoteLocker.Common.Model
{
    /// <summary>
    /// Account entity
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Username
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// Hashing password
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Identify code (Unique)
        /// </summary>
        public String IdentifyCode { get; set; }

        /// <summary>
        /// Create empty instance
        /// </summary>
        public Account() { }

        /// <summary>
        /// Create new instance with specific information
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="Password">Password</param>
        /// <param name="IdentifyCode">Identify code</param>
        public Account(String Username, String Password, String IdentifyCode)
        {
            this.Username = Username;
            this.Password = Password;
            this.IdentifyCode = IdentifyCode;
        }

        /// <summary>
        /// Compare to Account object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Account)
            {
                Account account = (Account)obj;

                if (account.IdentifyCode.Equals(this.IdentifyCode))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get Account hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
