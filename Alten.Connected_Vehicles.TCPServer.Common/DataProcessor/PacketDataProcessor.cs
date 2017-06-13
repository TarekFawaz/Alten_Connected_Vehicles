using Alten.Connected_Vehicles.TCPServer.Common.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.DataProcessor
{
    public class PacketDataProcessor : IDataProcessor
    {
        /// <summary>
        /// Representing Data members as quick access properties
        /// </summary>
        #region Data Members

       
        public string CheckSum { get; set; }

        public MessageDirection Direction { get; set; }

        public int Length { get; set; }
        
        public string UnitRegNo { get; set; }

        public bool UnitStatus { get; set; }

        public byte[] RawMessage { get; set; }


        #endregion

        #region CTOR

        public PacketDataProcessor(byte[] pRawMessage)
        {
           
            RawMessage = pRawMessage;
        }

        #endregion 


        #region IDataProcessor Implemented
        /// <summary>
        /// Process Data method that parse the binary data into readable values
        /// </summary>
        public void ProcessData()
        {
            if (RawMessage.Length > 0)
            {
                Length = RawMessage.Length;
                Direction = GetMessageDirection();
                UnitRegNo = GetMessageRegNo();
                UnitStatus = GetMessageStatus();
            }
        }


        #endregion



        #region Parser Methods
        /// <summary>
        /// method to get the message direction, message could be Send to Device or send to server otherwise it will considered unkonw message
        /// </summary>
        /// <returns>retrun the message direction</returns>
        private MessageDirection GetMessageDirection()
        {
            try
            {
                byte[] typeBytes = new byte[2];
                ASCIIEncoding encoder = new ASCIIEncoding();
                Array.Copy(RawMessage, 0, typeBytes, 0, 2);
                if (encoder.GetString(typeBytes) == "@@")
                {
                    return MessageDirection.SERVER_TO_UNIT;
                }
                if (encoder.GetString(typeBytes) == "$$")
                {
                    return MessageDirection.UNIT_TO_SERVER;
                }
                return MessageDirection.UNKNOWN;
            }
            catch
            {
                throw new Exception("Invalid message structure or unexpected data");
            }
        }
        /// <summary>
        /// Get vehicle Reg No
        /// </summary>
        /// <returns>string represent the Reg Number</returns>
        private  string GetMessageRegNo()
        {
            try
            {
                byte[] UnitIDBytes = new byte[6];
                ASCIIEncoding encoder = new ASCIIEncoding();
                Array.Copy(RawMessage, 3, UnitIDBytes, 0, 6);
                return BitConverter.ToString(UnitIDBytes, 0);
            }
            catch
            {
                throw new Exception("Invalid message structure or unexpected data");
            }
        }

       /// <summary>
       /// get the vehicle status
       /// </summary>
       /// <returns>get the status</returns>
        private bool GetMessageStatus()
        {
            byte[] lengthBytes = new byte[1];
            Array.Copy(RawMessage, 9, lengthBytes, 0, 1);

            return BitConverter.ToBoolean(lengthBytes, 0);
        }

        #endregion 
    }
}
