using Alten.Connected_Vehicles.DAL;
using Alten.Connected_Vehicles.DTO;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.BLL.Interfaces
{
    /// <summary>
    /// Business layer to maintain the transalted transaction message
    /// </summary>
    public interface ITransactionService
    {
        bool AddTransaction(TransactionDTO transactionObject);
        PagedList<Transaction> GetVehicleTransactions(string RegNo);
        List<TransactionDTO> GetVehicleTransctionsDuration(string RegNo, DateTime datefrom, DateTime dateto);
    }
}
