using System;
using Alten.Connected_Vehicles.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alten.Connected_Vehicles.WebAPI.Tests.Controllers
{
    [TestClass]
    public class VehicleControllerTest : TestBase
    {
        [TestMethod]
        public void GetCustomerVehicles_Pass()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var vehiclesList= vehicleController.GetCustomerVehicles("EFB499FA-B179-4B99-9539-6925751F1FB6");

            Assert.IsNotNull(vehiclesList);
            Assert.AreEqual(vehiclesList.Content.Count, 3);
        }
        

        [TestMethod]
        public void GetCustomerVehicles_Fail()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var vehiclesList = vehicleController.GetCustomerVehicles("EFB499FA-B199-4C99-9539-6925751F1FB6");

            Assert.IsNotNull(vehiclesList);
            Assert.AreEqual(vehiclesList.Content.Count, 0);
        }


        [TestMethod]
        public void GetVehicleByStatus_Pass()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var vehiclesList = vehicleController.GetVehicleByStatus(false);

            Assert.IsNotNull(vehiclesList);
            Assert.AreEqual(vehiclesList.Content.Count, 7);
        }


        [TestMethod]
        public void GetVehicleByStatus_Fail()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var vehiclesList = vehicleController.GetVehicleByStatus(true);

            Assert.IsNotNull(vehiclesList);
            Assert.AreEqual(vehiclesList.Content.Count, 0);
        }

        [TestMethod]
        public void UpdateVehicleStatus_Pass()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var result = vehicleController.UpdateVehicleStatus("ABC123",true);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateVehicleStatus_VerifyUpdate()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var vehiclesList = vehicleController.GetVehicleByStatus(true);

            Assert.IsNotNull(vehiclesList);
            Assert.AreEqual(vehiclesList.Content.Count,1);
            //Resting the Status
            var result = vehicleController.UpdateVehicleStatus("ABC123", false);
        }


        [TestMethod]
        public void UpdateVehicleStatus_Fail()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var result = vehicleController.UpdateVehicleStatus("XYZ123", true);

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void UpdateVehicleStatus_VerifyNoUpdate()
        {
            VehicleController vehicleController = new VehicleController(_vehicleService);

            var vehiclesList = vehicleController.GetVehicleByStatus(true);

            Assert.IsNotNull(vehiclesList);
            Assert.AreEqual(vehiclesList.Content.Count, 0);
            
        }
    }
}
