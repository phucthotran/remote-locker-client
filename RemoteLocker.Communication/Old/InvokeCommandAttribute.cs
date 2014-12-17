using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication.Old
{
    [AttributeUsage(AttributeTargets.Method, Inherited=false)]
    public class InvokeCommandAttribute : System.Attribute
    {
        public Command CommandType { get; set; }

        public InvokeCommandAttribute(Command CommandType)
        {
            this.CommandType = CommandType;
        }
    }
}
