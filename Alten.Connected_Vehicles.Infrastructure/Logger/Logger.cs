using System;
using System.IO;


namespace Alten.Connected_Vehicles.Infrastructure.Logger
{
    public class Logger: ILog
    {

        #region Data members 
        /// <summary>
        /// that will be used for naiive locking issue resolving in Multi-therad Operations
        /// </summary>
        static object locker = new object();
        string _logFolderPath;
        string _logFilePath;
        string _archivedLogFilePath;
        #endregion


        #region CTOR
        public Logger()
        {

        }
        #endregion

        #region Ilog
        /// <summary>
        /// Log to file with default configurations
        /// </summary>
        /// <param name="logString">String to be logged</param>
        public void LogToFile(string logEntry)
        {
            lock (locker) // Naiive Locking :) 
            {
                _logFolderPath = string.Format("{0}\\logs", AppDomain.CurrentDomain.BaseDirectory);
               _logFilePath =string.Format("{0}\\Servicelog.txt",_logFolderPath ); // define default log file storage path 
                _archivedLogFilePath = string.Format("{0}\\Servicelog", _logFolderPath)+"{0}.txt"; // define archived log files name pattern
                if (File.Exists(_logFilePath)) // Log file exist 
                {
                    FileInfo finfo = new FileInfo(_logFilePath);
                    long FileSzinKB = (finfo.Length / 1024);
                    if (FileSzinKB >= 250) // archive log files more than or equal to 250 KB
                    {
                        File.Copy(_logFilePath, string.Format(_archivedLogFilePath, Guid.NewGuid().ToString().Replace("-", ""))); // Archive old log file that reach the maximum size 
                        // Using GUID to Avoid duplications
                        File.Delete(_logFilePath);// delete the original file that reached the limit

                    }
                }

                if (!Directory.Exists(_logFolderPath)) // check if log directory exists 
                {
                    Directory.CreateDirectory(_logFolderPath); // Create the log file with default configurations
                }

                using (StreamWriter file = new StreamWriter(_logFilePath, true)) // write file using stream writer with Append mode enabled
                {
                    file.WriteLine(string.Format("[{0}] - {1} \r\n ", DateTime.Now, logEntry)); // stamping the log entry with date time 
                }
            }
        }

        public void LogToFile(string logEntry, string Path, string LogFolderName, string FileName, int MaxFileSize)
        {
            lock (locker) // Naiive Locking :) 
            {
                 _logFolderPath = string.Format("{0}\\{1}", Path, LogFolderName);
                 _logFilePath = string.Format("{0}\\{1}.txt",_logFolderPath,FileName);  // define log file storage path based on given congiurations
                 _archivedLogFilePath = string.Format("{0}\\{1}", _logFolderPath, FileName)+"{0}.txt"; // define archived log files name pattern
                if (File.Exists(_logFilePath)) // Log file exist 
                {
                    FileInfo finfo = new FileInfo(_logFilePath);
                    long FileSzinKB = (finfo.Length / 1024);
                    if (FileSzinKB >= MaxFileSize) // archive log files more than or equal to Max size in  KB
                    {
                        File.Copy(_logFilePath, string.Format(_archivedLogFilePath, Guid.NewGuid().ToString().Replace("-", ""))); // Archive old log file that reach the maximum size 
                        // Using GUID to Avoid duplications
                        File.Delete(_logFilePath);// delete the original file that reached the limit

                    }
                }

                if (!Directory.Exists(_logFolderPath)) // check if log directory exists 
                {
                    Directory.CreateDirectory(_logFolderPath); // Create the log file with default configurations
                }

                using (StreamWriter file = new StreamWriter(_logFilePath, true)) // write file using stream writer with Append mode enabled
                {
                    file.WriteLine(string.Format("[{0}] - {1} \r\n ", DateTime.Now, logEntry)); // stamping the log entry with date time 
                }
            }
        }
        #endregion

    }
}
