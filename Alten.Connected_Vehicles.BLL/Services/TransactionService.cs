using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_vehicle.Model;
using Alten.Connected_Vehicles.DTO;
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
   public class TransactionService: ITransactionService
    {
        #region Data members 
        IRepository<Transaction> Repo;
        IUnitOfWork<Transaction> UoM;
        Logger log = new Logger();
        #endregion

        #region CTOR
        public TransactionService(IRepository<Transaction> pRepo, IUnitOfWork<Transaction> pUoM)
        {
            Repo = pRepo;
            UoM = pUoM;
        }
        #endregion 

        #region Implement ITransactionService protocol

        public bool AddTransaction(TransactionDTO transactionObject)
        {
            bool bRet = false;
            try
            {
                if (transactionObject != null)
                {
                    using (UoM)
                    {
                        Repo.Add(transactionObject.GetDALObj());
                    }
                    bRet = true;
                }
            }
            catch(Exception ex)
            {
                log.LogToFile(string.Format("Transaction BLL Exception: Message {0} - Stack Trace{1}", ex.Message, ex.StackTrace));
            }

            return bRet;
        }
        /// <summary>
        /// Get paged list of all transactions for specific vehicle, that will be used 
        /// for playing back connected vehicle historical status
        /// </summary>
        /// <param name="RegNo"></param>
        /// <returns></returns>
       public  PagedList<Transaction> GetVehicleTransactions(string RegNo)
        {
            return Repo.Paginate(Repo.All().Where(t => t.RegNo == RegNo));
        }
        /// <summary>
        /// Get List of transactions during specific duration for specific Vehicle
        /// </summary>
        /// <param name="RegNo"></param>
        /// <param name="datefrom"></param>
        /// <param name="dateto"></param>
        /// <returns></returns>
        public List<TransactionDTO> GetVehicleTransctionsDuration(string RegNo, DateTime datefrom, DateTime dateto)
        {
            return TransactionDTO.GetList(Repo.All().Where(t => t.RegNo == RegNo & t.EntryDate >= datefrom & t.EntryDate <= dateto).ToList()).ToList();
        }
#endregion
         

    }
}
