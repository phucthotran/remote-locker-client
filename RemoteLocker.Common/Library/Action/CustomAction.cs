using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Common.Library.Action
{
    public class CustomAction
    {
        public delegate void ActionHandler();

        public static void Call(Func<bool> func, ActionHandler completedAction)
        {
            if (func())
                completedAction();
        }
    }
}
