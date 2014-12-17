using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteLocker.Communication
{
    /// <summary>
    /// Command packet for sending via network
    /// </summary>
    public class CommandPacket
    {
        /// <summary>
        /// Init packet with default instance
        /// </summary>
        public static CommandPacket EmptyPacket = new CommandPacket();

        /// <summary>
        /// Command type to invoked
        /// </summary>
        public Command CommandType { get; private set; }

        /// <summary>
        /// Plain text data (Input)
        /// </summary>
        public String Input { get; private set; }

        /// <summary>
        /// Default instance
        /// </summary>
        private CommandPacket()
        {
            this.CommandType = new Command(Command.NULL);
        }

        /// <summary>
        /// New instance with encode data
        /// </summary>
        /// <param name="EncodeData">Encode data</param>
        public CommandPacket(byte[] EncodeData, int ReceivedByte)
        {
            this.CommandType = new Command(Encoding.Default.GetString(EncodeData, 0, 40)); //First 40 bytes is Command.Value hash string
            this.Input = Encoding.Default.GetString(EncodeData, 40, ReceivedByte - 40); //Remain byte is PlainData input
        }

        /// <summary>
        /// New instance with specific Command type
        /// </summary>
        /// <param name="CommandType">Command type</param>
        public CommandPacket(Command CommandType, String Input)
        {
            this.CommandType = CommandType;
            this.Input = Input;
        }

        /// <summary>
        /// Convert packet to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            List<Byte> data = new List<Byte>();

            data.AddRange(Encoding.Default.GetBytes(this.CommandType.Value));
            data.AddRange(Encoding.Default.GetBytes(this.Input));

            return data.ToArray();
        }
    }
}
