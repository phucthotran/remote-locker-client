using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication.Old
{
    public enum Command
    {
        Unlock,
        Lock,
        Login,
        Logout,
        Shutdown,
        Null
    }
}
