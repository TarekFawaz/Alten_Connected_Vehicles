using Alten.Connected_Vehicles.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alten.Connected_Vehicles.WebAPI.Controllers
{
    public class RawTransactionsController : ApiController
    {
        private IRawTransactionService _rawTransactionService;

        public RawTransactionsController(IRawTransactionService rawTransactionService)
        {
            _rawTransactionService = rawTransactionService;
        }

        /// <summary>
        /// Simple Add 
        /// Note Exceptions will thrown from the business layer and information will logged into logger file
        /// </summary>
        /// <param name="RawData"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddRawTransaction([FromBody] string RawData)
        {
            if(!string.IsNullOrEmpty(RawData))
            {
                return _rawTransactionService.AddRawTransaction(Convert.FromBase64String(RawData));
            }
            return false;
            
        }
    }
}
