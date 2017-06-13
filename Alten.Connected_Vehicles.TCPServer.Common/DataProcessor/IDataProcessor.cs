using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.DataProcessor
{
    /// <summary>
    /// Protocol for data processor 
    /// </summary>
    interface IDataProcessor
    {
        /// <summary>
        /// currentlly the IDataprocessor should at least include Process Data method
        /// </summary>
        void ProcessData();
       

    }
}
