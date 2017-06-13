using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_Vehicles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Alten.Connected_Vehicles.WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
/// <summary>
/// using DryIOC will resolve the dependancies 
/// </summary>
        private ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get list of cutomers as JSON
        /// </summary>
        /// <returns></returns>
        public JsonResult<List<CustomerDTO>> Get()
        {
            return Json(_customerService.GetCustomers());
        }

       
    }
}
