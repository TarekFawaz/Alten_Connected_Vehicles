using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_Vehicles.DAL;
using Alten.Connected_Vehicles.Infrastructure.Logger;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using Alten.Connected_Vehicles.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.BLL.Services
{
   public  class RawTransactionService : IRawTransactionService 
    {
        #region Data members 
        IRepository<RawTransction> Repo;
        IUnitOfWork<RawTransction> UoM;
        Logger log = new Logger();
        #endregion

        #region CTOR
        public RawTransactionService(IRepository<RawTransction> pRepo,IUnitOfWork<RawTransction> pUoM)
        {
            Repo = pRepo;
            UoM = pUoM;
        }
        #endregion

        #region Impelement IRawTransactionService protocol
        /// <summary>
        /// Basic data entry for Raw data transmited by the cars
        /// </summary>
        /// <param name="RawData">Raw Data message as recieved</param>
        /// <returns></returns>
        public bool AddRawTransaction(byte[] RawData)
        {
            bool bRet = false;
            if (RawData != null)
            {
                try
                {
                    using (UoM)
                    {
                        Repo.Add(new RawTransction()
                        {
                            
                            RawData = RawData,
                            EntryDate = DateTime.Now
                        });
                    }
                    bRet = true;
                }
                catch (Exception ex)
                {
                    log.LogToFile(string.Format("RawTransaction BLL Exception: Message {0} - Stack Trace{1}", ex.Message, ex.StackTrace));
                }
            }
            return bRet;
        }
        #endregion
    }
}
