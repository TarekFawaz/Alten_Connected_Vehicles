using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_Vehicles.DAL;
using Alten.Connected_Vehicles.DTO;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using Alten.Connected_Vehicles.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.BLL.Services
{
    public class VehicleService: IVehicleService
    {
        #region Data members 
        IRepository<Vehicle> Repo;
        IUnitOfWork<Vehicle> UoM;

        #endregion

        #region CTOR
        public VehicleService(IRepository<Vehicle> pRepo,IUnitOfWork<Vehicle> pUoM)
        {
            Repo = pRepo;
            UoM = pUoM;
        }

        #endregion

        #region IvehicleService Implementation 
        /// <summary>
        /// Get list of vehicles filterd by customer 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<VehicleDTO> GetCustomerVehicles(Guid customerID)
        {
            /// Repository Implementation based on Iquerable, the query send to DB engine
            return VehicleDTO.GetList(Repo.All().Where(v => v.CustomerID == customerID).ToList()).ToList();
        }
        /// <summary>
        /// Get vehicles filterd by Status 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<VehicleDTO> GetVehicleByStatus(bool status)
        {
            return VehicleDTO.GetList(Repo.All().Where(v => v.ActiveStatus == status).ToList()).ToList();
        }

        /// <summary>
        /// Update vehicle status 
        /// </summary>
        /// <param name="RegNo">Vehicle RegNo</param>
        /// <param name="status">Status to update </param>
        /// <returns></returns>
        public bool UpdateVehicle(string RegNo, bool status)
        {
            bool bRet = false;
            var vehicleobj = Repo.FindById(RegNo);
            if (vehicleobj != null)
            {
                using (UoM)
                {
                    vehicleobj.ActiveStatus = status;
                    vehicleobj.LastUpdateTime = DateTime.Now;
                    Repo.Update(vehicleobj);
                }
                bRet = true;
            }

             return bRet;
        }
        #endregion

    }
}
