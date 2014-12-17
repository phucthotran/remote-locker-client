using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Common.Global
{
    /// <summary>
    /// Global constant variable
    /// </summary>
    public class CommonConstant
    {
        /// <summary>
        /// Remote locker's data path
        /// </summary>
        public const String REMOTE_LOCKER_DATA = @"remotelocker.rldb";

        /// <summary>
        /// Login signal
        /// </summary>
        public const String LOGIN_ACTION = "LoginAction";

        /// <summary>
        /// Unlock signal
        /// </summary>
        public const String UNLOCK_ACTION = "UnlockAction";

        /// <summary>
        /// Change account signal
        /// </summary>
        public const String CHANGE_ACTION = "ChangeAction";

        /// <summary>
        /// Unlock request signal
        /// </summary>
        public const String REQUEST_ACTION = "RequestAction";

        /// <summary>
        /// Logout signal
        /// </summary>
        public const String LOGOUT_ACTION = "LogoutAction";

        /// <summary>
        /// Shutdown signal
        /// </summary>
        public const String SHUTDOWN_ACTION = "ShutdownAction";

        /// <summary>
        /// Remoting communication's port
        /// </summary>
        public const int COMMUNICATION_PORT = 63814;

        /// <summary>
        /// Remoting communication's receive size
        /// </summary>
        public const int COMMUNICATION_RECEIVE_SIZE = 128;
    }
}
