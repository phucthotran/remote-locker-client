using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using RemoteLocker.Common.Global;

namespace RemoteLocker.Communication
{
    /// <summary>
    /// Communication via TCP/IP
    /// </summary>
    public class TcpCommunication : ICommunicationProvider
    {
        private IPAddress ipAddress;
        private IPEndPoint ipEndPoint;
        private IPAddress remoteIpAddress;
        private IPEndPoint remoteIpEndPoint;
        private EndPoint remoteEndPoint;
        private Socket skListener;
        private Socket skClient;
        private byte[] data;
        private Object communicationObject;
        private bool canReaction = true;
        private ICommandSender commandSender;

        public bool CanReaction
        {
            get { return canReaction; }
            set { canReaction = value; }
        }

        public ICommandSender CommandSender
        {
            get { return this.commandSender; }
            set { this.commandSender = value; }
        }

        /// <summary>
        /// Create new TCP Communication with some config
        /// </summary>
        public TcpCommunication(Object CommunicationObject)
        {
            communicationObject = CommunicationObject;

            data = new byte[CommonConstant.COMMUNICATION_RECEIVE_SIZE];

            ipAddress = IPAddress.Any;
            ipEndPoint = new IPEndPoint(ipAddress, CommonConstant.COMMUNICATION_PORT);
            skListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            remoteIpAddress = IPAddress.Any;
            remoteIpEndPoint = new IPEndPoint(remoteIpAddress, 0);
            remoteEndPoint = new IPEndPoint(remoteIpAddress, 0);
        }

        /// <summary>
        /// Execute an action by Command type with input is plain text data
        /// </summary>
        /// <param name="CommandType">Command type</param>
        /// <param name="Input">Plain text data</param>
        public void Execute(Command CommandType, String Input)
        {
            CommandResolve.Invoke(communicationObject, CommandType, Input);
        }

        /// <summary>
        /// Send a command to client
        /// </summary>
        public void SendCommand()
        {
            CommandPacket cmdPacket = new CommandPacket(new Command(commandSender.GetCommand()), commandSender.GetInput());
            
            if(skClient != null && skClient.Connected)
                skClient.SendTo(cmdPacket.ToBytes(), remoteEndPoint);
        }

        /// <summary>
        /// Start communication
        /// </summary>
        public void Start()
        {
            ConnectionEstablish();
        }

        /// <summary>
        /// Stop communication
        /// </summary>
        public void Stop()
        {
            skClient.Disconnect(true);
            skClient.Close();

            skListener.Disconnect(true);
            skListener.Close();

            skClient = null;
            skListener = null;
        }

        /// <summary>
        /// Disconnect client away from server
        /// </summary>
        public void ClientDisconnect()
        {
            skClient.Disconnect(true);
            skClient.Close();

            //Ready for new connection
            skListener.BeginAccept(new AsyncCallback(OnClientConnect), skListener);
        }

        /// <summary>
        /// Establish TCP/IP connection
        /// </summary>
        void ConnectionEstablish()
        {
            skListener.Bind(ipEndPoint);
            skListener.Listen(1);

            skListener.BeginAccept(new AsyncCallback(OnClientConnect), skListener);
        }

        /// <summary>
        /// Waiting for Client and establish connection for it
        /// </summary>
        /// <param name="asyncResult"></param>
        void OnClientConnect(IAsyncResult asyncResult)
        {
            skClient = skListener.EndAccept(asyncResult);
            skClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            skClient.BeginReceiveFrom(data, 0, data.Length, SocketFlags.None, ref remoteEndPoint, new AsyncCallback(OnClientRequest), null);
        }

        /// <summary>
        /// Proccess Client's requests
        /// </summary>
        /// <param name="asyncResult"></param>
        void OnClientRequest(IAsyncResult asyncResult)
        {
            CommandPacket cmdPacket = CommandPacket.EmptyPacket;
            int receivedByte = 0;

            try
            {
                receivedByte = skClient.EndReceiveFrom(asyncResult, ref remoteEndPoint);

                if (receivedByte > 0)
                {
                    cmdPacket = new CommandPacket(data, receivedByte);
                    //Execute action by specific Command type
                    Execute(cmdPacket.CommandType, cmdPacket.Input);
                }

                if (!canReaction)
                {
                    skListener.BeginAccept(new AsyncCallback(OnClientConnect), skListener);
                    return;
                }

                if (skClient.Connected)
                    skClient.BeginReceiveFrom(data, 0, data.Length, SocketFlags.None, ref remoteEndPoint, new AsyncCallback(OnClientRequest), null);                    
            }
            catch (SocketException ex)
            {
                //If socket raise 'Shutdown/ConnectionReset' error, just disconnect client and waiting for new Client
                if (ex.SocketErrorCode == SocketError.Shutdown || ex.SocketErrorCode == SocketError.ConnectionReset)
                {
                    skClient.Disconnect(true);
                    skClient.Close();
                    skListener.BeginAccept(new AsyncCallback(OnClientConnect), skListener);
                }
            }
        }
    }
}
