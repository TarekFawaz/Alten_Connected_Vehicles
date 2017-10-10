using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alten.Connected_vehicle.Model;

/// <summary>
/// Data transfer Objects, used for simplifying DAL obejcts to the client to deliver only the data scope needed for the client 
/// Exposing DAL objects directly to is not recommended 
/// </summary>
namespace Alten.Connected_Vehicles.DTO
{
    [Serializable]
    public class CustomerDTO
    {
        #region properties
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<VehicleDTO> Vehicles { get;set;}

        #endregion

        #region CTORS
       
        /// <summary>
        /// Full Constructor for mapping
        /// Avoid using Automapping for the sake of the performance 
        /// </summary>
        /// <param name="customerDAL"></param>
        public CustomerDTO(Customer customerDAL)
        {
            this.ID = customerDAL.ID;
            this.Name = customerDAL.Name;
            this.Address = customerDAL.Address;
            this.Vehicles = new List<VehicleDTO>();
            if(customerDAL.Vehicles.Count>0)
            {
                this.Vehicles = VehicleDTO.GetList(customerDAL.Vehicles);
            }
        }
        #endregion

        #region Mapping helpers 
        /// <summary>
        /// Map Collection of DAL objects Into List of DTOs
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public static IEnumerable<CustomerDTO> GetList(ICollection<Customer> Collection)
        {
            List<CustomerDTO> list = new List<CustomerDTO>();
            foreach(Customer cus in Collection)
            {
                list.Add(new CustomerDTO(cus));
            }
            return list;
        }


        #endregion 


    }
}
