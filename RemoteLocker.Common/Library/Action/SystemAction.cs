using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Common.Library.Action
{
    public class SystemAction
    {
        public static void Shutdown()
        {
            System.Diagnostics.Process.Start("Shutdown", "/s /t 0");
        }

        public static String GetComputerName()
        {
            return System.Environment.MachineName;
        }
    }
}
