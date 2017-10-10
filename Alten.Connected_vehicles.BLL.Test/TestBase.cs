using System;
using DryIoc;
using Effort;
using Alten.Connected_Vehicles.BLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Alten.Connected_vehicle.Model;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using Alten.Connected_Vehicles.Infrastructure.UnitOfWork;
using Alten.Connected_Vehicles.MSSQLRepository;
using Alten.Connected_Vehicles.BLL.Services;
using System.Data.Common;

namespace Alten.Connected_Vehicles.BLL.Test
{
    
    public class TestBase
    {
        protected IVehicleService _vehicleService;
        protected ICustomerService _customerService;
        
        public TestBase()
        {
            

            ///////////////////// DI based on DRY IOC/////////////////////////////
            var container = new Container();



            container.Register<DbContext, Connected_Vehicles_Models>(Reuse.Singleton);

            container.Register(typeof(IRepository<>), typeof(MSSQLRepository<>), setup: Setup.With(allowDisposableTransient: true));
            container.Register(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), setup: Setup.With(allowDisposableTransient: true));

           
            container.Register<ICustomerService, CustomerService>();
            container.Register<IVehicleService, VehicleService>();
           


            _customerService=container.Resolve<ICustomerService>();
            _vehicleService = container.Resolve<IVehicleService>();
            /////////////End of DI Configuration ////////////////////////

        }

    }

    
}
