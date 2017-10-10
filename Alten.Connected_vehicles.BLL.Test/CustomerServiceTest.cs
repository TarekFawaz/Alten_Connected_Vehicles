using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alten.Connected_Vehicles.BLL.Interfaces;

namespace Alten.Connected_Vehicles.BLL.Test
{
    [TestClass]
    public class CustomerServiceTest : TestBase
    {

       
        [TestMethod]
        public void GetCustomers_Pass()
        {
            var customersList=_customerService.GetCustomers();

            Assert.AreEqual(customersList.Count, 3);
        }

        


    }
}
