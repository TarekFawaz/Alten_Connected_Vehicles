using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alten.Connected_Vehicles.BLL.Test
{
    [TestClass]
    public class VehicleServiceTest :TestBase
    {
        #region Data Retrival Cases
        [TestMethod]
        public void GetCustomerVehicles_Pass()
        {
            var vehiclesList= _vehicleService.GetCustomerVehicles(Guid.Parse("EFB499FA-B179-4B99-9539-6925751F1FB6"));
            Assert.AreEqual(vehiclesList.Count,3);
        }

        [TestMethod]
        public void GetCustomerVehicles_Fail()
        {
            var vehiclesList = _vehicleService.GetCustomerVehicles(Guid.Parse("26643b56-c1b4-4d85-af48-98677dfb0d76"));
            Assert.AreEqual(vehiclesList.Count, 0);
        }

        [TestMethod]
        public void GetVehicleByStatus_Pass()
        {
            var vehiclesList = _vehicleService.GetVehicleByStatus(false);
            Assert.AreEqual(vehiclesList.Count, 7);
        }

        [TestMethod]
        public void GetVehicleByStatus_Fail()
        {
            var vehiclesList = _vehicleService.GetVehicleByStatus(true);
            Assert.AreEqual(vehiclesList.Count, 0);
        }
        #endregion

        #region Update Cases
        [TestMethod]
        public void UpdateVehicle_Pass()
        {
            var result = _vehicleService.UpdateVehicle("ABC123",true);
            Assert.AreEqual(result, true);
                        
        }

        [TestMethod]
        public void UpdateVehicle_EnsureRecordUpdate()
        {
            
            var vehiclesList = _vehicleService.GetVehicleByStatus(true);
            Assert.AreEqual(vehiclesList.Count, 1);
            // reset the record to its original state
            _vehicleService.UpdateVehicle("ABC123", false);
        }
        [TestMethod]
        public void UpdateVehicle_Fail()
        {
            var result = _vehicleService.UpdateVehicle("XYZ123", true);
            Assert.AreEqual(result, false);
            
        }

        [TestMethod]
        public void UpdateVehicle_EnsureNoRecordsUpdate()
        {
            var vehiclesList = _vehicleService.GetVehicleByStatus(true);
            Assert.AreEqual(vehiclesList.Count, 0);
        }



        #endregion

    }
}
