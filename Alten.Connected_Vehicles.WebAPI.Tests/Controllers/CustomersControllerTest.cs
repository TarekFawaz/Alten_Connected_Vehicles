using System;
using Alten.Connected_Vehicles.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alten.Connected_Vehicles.WebAPI.Tests.Controllers
{
    [TestClass]
    public class CustomersControllerTest : TestBase
    {
        [TestMethod]
        public void Get()
        {
            CustomersController customerController=new CustomersController(_customerService);

            var customerList=customerController.Get();

            Assert.IsNotNull(customerList);
            Assert.AreEqual(customerList.Content.Count,3);

        }
    }
}
