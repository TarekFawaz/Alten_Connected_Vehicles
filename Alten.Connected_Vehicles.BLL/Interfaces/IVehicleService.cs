using Alten.Connected_Vehicles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.BLL.Interfaces
{
    public interface IVehicleService
    {
        List<VehicleDTO> GetCustomerVehicles(Guid customerID);
        List<VehicleDTO> GetVehicleByStatus(bool status);
        bool UpdateVehicle(string RegNo,bool status);
    }
}
