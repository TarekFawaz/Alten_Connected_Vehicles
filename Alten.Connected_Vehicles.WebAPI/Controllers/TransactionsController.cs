using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_Vehicles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alten.Connected_Vehicles.WebAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        private ITransactionService _transactionService;
        private IVehicleService _vehicleService;

        public TransactionsController(ITransactionService transactionService, IVehicleService vehicleService)
        {
            _transactionService = transactionService;
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Simple Add 
        /// Note Exceptions will thrown from the business layer and information will logged into logger file
        /// </summary>
        /// <param name="RawData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddRawTransaction(TransactionDTO transaction)
        {
            if (transaction!=null)
            {
                if( _transactionService.AddTransaction(transaction))
                {
                   return _vehicleService.UpdateVehicle(transaction.RegNo, transaction.Status);
                }

            }
            return false;

        }
    }
}
