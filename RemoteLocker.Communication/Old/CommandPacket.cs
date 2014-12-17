using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication.Old
{
    public class CommandPacket
    {
        public static CommandPacket EmptyPacket = new CommandPacket();

        public Command CommandType { get; private set; }

        public CommandPacket()
        {
            this.CommandType = Command.Null;
        }

        public CommandPacket(byte[] EncodeData)
        {
            this.CommandType = (Command)BitConverter.ToInt32(EncodeData, 0);
        }

        public CommandPacket(Command CommandType)
        {
            this.CommandType = CommandType;
        }

        public byte[] ToByte()
        {
            List<Byte> data = new List<Byte>();
            data.AddRange(BitConverter.GetBytes((int)CommandType));

            return data.ToArray();
        }
    }
}
