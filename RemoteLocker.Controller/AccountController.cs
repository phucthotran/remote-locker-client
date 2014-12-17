using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RemoteLocker.Common.Model;
using RemoteLocker.Common.Library.DataAccess;
using RemoteLocker.Common.Global;

namespace RemoteLocker.Controller
{
    /// <summary>
    /// Account's controller
    /// </summary>
    public class AccountController
    {
        /// <summary>
        /// Default instance
        /// </summary>
        public AccountController() { }

        /// <summary>
        /// Fetch account data from file
        /// </summary>
        /// <returns></returns>
        public Account Fetch()
        {            
            String[] data = PlainTextData.FetchFrom(CommonConstant.REMOTE_LOCKER_DATA, ';');

            Account account = new Account();
            account.Username = data[0];
            account.Password = data[1];
            account.IdentifyCode = data[2];

            return account;
        }

        /// <summary>
        /// Save account information to file
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public bool Save(Account Account)
        {
            return PlainTextData.SaveTo(CommonConstant.REMOTE_LOCKER_DATA, ';',   
                    Account.Username,
                    Account.Password, 
                    Account.IdentifyCode
            );
        }

        /// <summary>
        /// Check username and password
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="Password">Password</param>
        /// <returns></returns>
        public bool Available(String Username, String Password)
        {
            Account account = Fetch();

            if (account.Username.Equals(Username) && account.Password.Equals(Password))
                return true;

            return false;
        }

        /// <summary>
        /// Check identify code
        /// </summary>
        /// <param name="IdentifyCode">Identify code (MD5 hash string without '-' character)</param>
        /// <returns></returns>
        public bool AvailableCode(String IdentifyCode)
        {
            Account account = Fetch();

            if (account.IdentifyCode.Equals(IdentifyCode))
                return true;

            return false;
        }
    }
}
