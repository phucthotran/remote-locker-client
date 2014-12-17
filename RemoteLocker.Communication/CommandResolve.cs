using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace RemoteLocker.Communication
{
    /// <summary>
    /// Command routing
    /// </summary>
    internal class CommandResolve
    {
        /// <summary>
        /// Invoke a Command type with approved Action (in Actions list)
        /// </summary>
        /// <param name="Actions">List of action</param>
        /// <param name="CommandType">Type of Command to invoke</param>
        public static void Invoke(Object CommunicationObject, Command CommandType, String Input)
        {
            MethodInfo invokeCommand = FindCommand(CommunicationObject, CommandType);

            if (invokeCommand == null)
                return;

            InvokeCommandAttribute commandAttr = (InvokeCommandAttribute)invokeCommand.GetCustomAttributes(typeof(InvokeCommandAttribute), false).First();

            switch (InvokeCommand(invokeCommand, CommunicationObject, Input))
            {
                case 1:
                    MethodInfo onOutputTrue = FindCommand(CommunicationObject, new Command(commandAttr.OnOutputTrue));
                    InvokeCommand(onOutputTrue, CommunicationObject, Input);
                    break;

                case -1:
                    MethodInfo onOutputFalse = FindCommand(CommunicationObject, new Command(commandAttr.OnOutputFalse));
                    InvokeCommand(onOutputFalse, CommunicationObject, Input);
                    break;

                case 0:                    
                    break;
            }
        }

        static int InvokeCommand(MethodInfo Command, Object CommunicationObject, String PlainData = "")
        {
            InvokeCommandAttribute commandAttr = (InvokeCommandAttribute)Command.GetCustomAttributes(typeof(InvokeCommandAttribute), false).First();
            bool HasInput = commandAttr.HasInput;
            bool HasOuput = commandAttr.HasOutput;

            if (HasInput && HasOuput)
                return (bool)Command.Invoke(CommunicationObject, new Object[] { PlainData }) ? 1 : -1;
            else if (HasInput && !HasOuput)
            {
                Command.Invoke(CommunicationObject, new Object[] { PlainData });
                return 0;
            }
            else if (!HasInput && HasOuput)
            {
                return (bool)Command.Invoke(CommunicationObject, null) ? 1 : -1;
            }
            else if(!HasInput && !HasOuput)
            {
                Command.Invoke(CommunicationObject, null);
                return 0;
            }

            return -1;
        }

        static MethodInfo FindCommand(Object CommunicationObject, Command CommandType)
        {
            int idx = 0;
            Type communicationObjectType = CommunicationObject.GetType();
            MethodInfo[] communicationObjectMethods = communicationObjectType.GetMethods();
            MethodInfo[] invokeMethods = communicationObjectMethods.Where<MethodInfo>(m => m.GetCustomAttributes(typeof(InvokeCommandAttribute), false).Count() > 0 ).ToArray();

            while (idx < invokeMethods.Length)
            {
                MethodInfo invokeMethod = invokeMethods[idx];
                InvokeCommandAttribute methodAttr = (InvokeCommandAttribute)invokeMethod.GetCustomAttributes(typeof(InvokeCommandAttribute), false).First();

                if (methodAttr.CommandType == CommandType.Value)
                    return invokeMethod;

                idx++;
            }

            return null;
        }
    }
}
