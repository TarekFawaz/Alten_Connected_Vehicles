using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.Server
{
    public class StateObject
    {
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];

    }

    public enum MessagesTypes { NORMAL_MSG, HEARTBEAT_MSG, App_MSG, IGNORE };

    public enum ServerConnectionState { Connected, Idle, Disconnected };

    public enum MessageDirection { UNIT_TO_SERVER, SERVER_TO_UNIT, UNKNOWN };

    /// <summary>
    /// Bit Converter Extension methods
    /// </summary>
    public class BitconverterExt
    {
        /// <summary>
        /// convert decimal value into bytes
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static byte[] GetBytes(decimal dec)
        {
            int[] bits = decimal.GetBits(dec);
            List<byte> bytes = new List<byte>();
            foreach (int i in bits)
            {
                bytes.AddRange(BitConverter.GetBytes(i));
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// Convert byte array to decimal
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static decimal ToDecimal(byte[] bytes)
        {
            if (bytes.Length != 0x10)
            {
                throw new Exception("A decimal must be created from exactly 16 bytes");
            }
            int[] bits = new int[4];
            for (int i = 0; i <= 15; i += 4)
            {
                bits[i / 4] = BitConverter.ToInt32(bytes, i);
            }
            return new decimal(bits);
        }



    }
}
