using Alten.Connected_Vehicles.DTO;
using System;
using System.Collections.Generic;


namespace Alten.Connected_Vehicles.BLL.Interfaces
{
    /// <summary>
    /// build the required business layer for customers 
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// as per as task scope I will only limit the protocol to List Customers
        /// </summary>
        /// <returns></returns>
        List<CustomerDTO> GetCustomers();
        

    }
}
