using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alten.Connected_vehicle.Model;

namespace Alten.Connected_Vehicles.DTO
{
    /// <summary>
    /// Data transfer Objects, used for simplifying DAL obejcts to the client to deliver only the data scope needed for the client 
    /// Exposing DAL objects directly to is not recommended 
    /// </summary>
    public class VehicleDTO
    {
        #region Properties
        public string ID { get; set; }
        public string RegNo { get; set; }
        public Nullable<System.Guid> CustomerID { get; set; }
        public System.DateTime LastUpdateTime { get; set; }
        public bool ActiveStatus { get; set; }

        #endregion

        #region CTORS
        public  VehicleDTO(Vehicle VehicleDAL)
        {
            this.ID = VehicleDAL.ID;
            this.RegNo = VehicleDAL.RegNo;
            this.CustomerID = VehicleDAL.CustomerID.Value;
            this.LastUpdateTime = VehicleDAL.LastUpdateTime.HasValue ? VehicleDAL.LastUpdateTime.Value : DateTime.Now;
            this.ActiveStatus = VehicleDAL.ActiveStatus.HasValue ? VehicleDAL.ActiveStatus.Value : false;
        }
        #endregion

        #region Mapping helpers 
        /// <summary>
        /// Map Collection of DAL objects Into List of DTOs
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public static IEnumerable<VehicleDTO> GetList(ICollection<Vehicle> Collection)
        {
            List<VehicleDTO> list = new List<VehicleDTO>();
            foreach (Vehicle veh in Collection)
            {
                list.Add(new VehicleDTO(veh));
            }
            return list;
        }


        #endregion 
    }
}
