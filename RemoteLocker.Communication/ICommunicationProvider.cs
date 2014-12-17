using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication
{
    /// <summary>
    /// Base class for Communication protocol
    /// </summary>
    public interface ICommunicationProvider
    {
        /// <summary>
        /// Command sender object
        /// </summary>
        ICommandSender CommandSender { get; set; }

        /// <summary>
        /// Execute an action by Command type with input is plain text data
        /// </summary>
        /// <param name="CommandType">Command type</param>
        /// <param name="PlainData">Plain text data</param>
        void Execute(Command CommandType, String PlainData);

        /// <summary>
        /// Send a command to client
        /// </summary>
        void SendCommand();               

        /// <summary>
        /// Start communication
        /// </summary>
        void Start();

        /// <summary>
        /// Stop communication
        /// </summary>
        void Stop();
    }
}
