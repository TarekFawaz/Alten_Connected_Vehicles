using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_vehicle.Model;
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
    public class CustomerService : ICustomerService
    {
        #region Data Members 
        //Exciplicit Define the DAL object within Repo, as it is tightly related to the business scope of this service
        IRepository<Customer> Repo;
       
        
        #endregion

        #region CTOR

        /// <summary>
        /// Parametrized Constructor , items will intalized through IoC resolver and DI
        /// </summary>
        /// <param name="pRepository"></param>
        public CustomerService(IRepository<Customer> pRepository)
        {
            Repo = pRepository;
            

        }

        #endregion 


        #region Interface Implementation 
       public  List<CustomerDTO> GetCustomers()
        {
            return CustomerDTO.GetList(Repo.All().ToList()).ToList();
        }
        
        

        #endregion 
    }
}
