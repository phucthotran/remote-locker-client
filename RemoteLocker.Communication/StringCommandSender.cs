using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication
{
    public class StringCommandSender : ICommandSender
    {
        private String commandType;
        private String input;
        private ICommunicationProvider provider;

        public ICommunicationProvider Provider
        {
            get { return this.provider; }
            set 
            {
                this.provider = value;
                this.provider.CommandSender = this;
            }
        }

        public String GetCommand()
        {
            return this.commandType;
        }

        public String GetInput()
        {
            return this.input;
        }

        public void Send(String CommandType, String Input = "")
        {
            this.commandType = CommandType;
            this.input = Input;
            this.provider.SendCommand();
        }
    }
}
