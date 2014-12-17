using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RemoteLocker.Common.Library.Action;

namespace RemoteLocker.Communication.Old
{
    public class CommandResolve
    {
        public static void Invoke(Command CommandType, ICommunication InvokeObject)
        {
            Type cmdResolveType = InvokeObject.GetType();
            MethodInfo[] methods = cmdResolveType.GetMethods();

            foreach (MethodInfo method in methods)
            {
                InvokeCommandAttribute attribute = null;

                object[] customAttributes = method.GetCustomAttributes(typeof(InvokeCommandAttribute), false);

                if (customAttributes != null && customAttributes.Length == 0)
                    continue;

                attribute = customAttributes.First() as InvokeCommandAttribute;

                if (attribute != null && attribute.CommandType.Equals(CommandType))
                {
                    method.Invoke(InvokeObject, null);
                    //CommandResolve cmdResolveObj = (CommandResolve)Activator.CreateInstance(typeof(CommandResolve), null);
                    //method.Invoke(cmdResolveObj, null);
                    break;
                }
            }
        }
    }
}
