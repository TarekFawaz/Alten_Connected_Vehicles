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
    public class VehicleController : ApiController
    {
        private IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        
        public JsonResult<List<VehicleDTO>> GetCustomerVehicles(string customerID)
        {
            return Json(_vehicleService.GetCustomerVehicles(Guid.Parse(customerID)));
        }
        

        public JsonResult<List<VehicleDTO>> GetVehicleByStatus(bool status)
        {
            return Json(_vehicleService.GetVehicleByStatus(status));
        }
        [HttpPost]
        [Route("UpdateVehicleStatus")]
        public bool UpdateVehicleStatus(string RegNo,bool status)
        {
            return _vehicleService.UpdateVehicle(RegNo, status);
        }
    }
}
