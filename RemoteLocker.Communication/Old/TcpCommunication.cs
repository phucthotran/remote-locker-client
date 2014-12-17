using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using RemoteLocker.Common.Global;
using System.Threading;

namespace RemoteLocker.Communication.Old
{
    public class TcpCommunication
    {
        private IPAddress ipAddress;
        private IPEndPoint ipEndPoint;
        private IPAddress remoteIpAddress;
        private IPEndPoint remoteIpEndPoint;
        private EndPoint remoteEndPoint;
        private Socket skListener;
        private Socket skClient;
        private byte[] data;
        private ICommunication communicationObj;        

        public TcpCommunication()
        {           
            data = new byte[CommonConstant.COMMUNICATION_RECEIVE_SIZE];

            ipAddress = IPAddress.Any;
            ipEndPoint = new IPEndPoint(ipAddress, CommonConstant.COMMUNICATION_PORT);
            skListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            remoteIpAddress = IPAddress.Any;
            remoteIpEndPoint = new IPEndPoint(remoteIpAddress, 0);
            remoteEndPoint = new IPEndPoint(remoteIpAddress, 0);

            Connect();
        }

        public void CommunicationObject(ICommunication CommunicationObject)
        {
            this.communicationObj = CommunicationObject;
        }

        void Connect()
        {
            skListener.Bind(ipEndPoint);
            skListener.Listen(1);

            skListener.BeginAccept(new AsyncCallback(OnClientConnect), skListener);
        }

        void OnClientConnect(IAsyncResult asyncResult)
        {
            skClient = skListener.EndAccept(asyncResult);
            skClient.BeginReceiveFrom(data, 0, data.Length, SocketFlags.None, ref remoteEndPoint, new AsyncCallback(OnClientRequest), null);
        }

        void OnClientRequest(IAsyncResult asyncResult)
        {
            CommandPacket cmdPacket = CommandPacket.EmptyPacket;
            int receivedByte = 0;

            try
            {
                receivedByte = skClient.EndReceiveFrom(asyncResult, ref remoteEndPoint);

                if (receivedByte > 0)
                {
                    cmdPacket = new CommandPacket(data);
                    CommandResolve.Invoke(cmdPacket.CommandType, communicationObj);
                }

                skClient.BeginReceiveFrom(data, 0, data.Length, SocketFlags.None, ref remoteEndPoint, new AsyncCallback(OnClientRequest), null);
            }
            catch(SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.Shutdown || ex.SocketErrorCode == SocketError.ConnectionReset)
                {
                    skClient.Disconnect(false);
                    skClient.Close();
                    skListener.BeginAccept(new AsyncCallback(OnClientConnect), skListener);
                }
            }
        }        
    }
}
