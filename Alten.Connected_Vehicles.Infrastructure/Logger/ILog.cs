using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.Infrastructure.Logger
{
    /// <summary>
    /// introducing Logger protocol 
    /// currentlly will limit  logging to a file 
    /// </summary>
    /// TODO: Set protocols for Async logging , and logging to different data stores
    interface ILog
    {
        /// <summary>
        /// Log Data to a file
        /// </summary>
        /// <param name="logEntry"></param>
        void LogToFile(string logEntry);

        /// <summary>
        /// Log to file with options
        /// </summary>
        /// <param name="logEntry">the string to be logged</param>
        /// <param name="Path">the Path where the log folder should be created</param>
        /// /// <param name="LogFolderName">the name of the folder that will hold the log files</param>
        /// <param name="FileName">log file name</param>
        /// <param name="MaxFileSize">Set the log file limites in KB</param>
        void LogToFile(string logEntry, string Path,string LogFolderName ,string FileName, int MaxFileSize);
        
    }
}
