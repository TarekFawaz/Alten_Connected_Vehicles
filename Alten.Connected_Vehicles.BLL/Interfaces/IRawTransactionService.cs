using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.BLL.Interfaces
{
    public interface IRawTransactionService
    {
        /// <summary>
        /// Add the recieved RAW data from the server 
        /// </summary>
        /// <returns></returns>
        bool AddRawTransaction(byte[] RawData);

    }
}
