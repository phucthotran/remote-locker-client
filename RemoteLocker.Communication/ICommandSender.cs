using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication
{
    /// <summary>
    /// Base class for Command Sender
    /// </summary>
    public interface ICommandSender
    {
        /// <summary>
        /// Communication provider object
        /// </summary>
        ICommunicationProvider Provider { get; set; }
        
        /// <summary>
        /// Get command value
        /// </summary>
        /// <returns></returns>
        String GetCommand();

        /// <summary>
        /// Get input
        /// </summary>
        /// <returns></returns>
        String GetInput();

        /// <summary>
        /// Send a command to Provider
        /// </summary>
        /// <param name="CommandType"></param>
        void Send(String CommandType, String Input = "");
    }
}
