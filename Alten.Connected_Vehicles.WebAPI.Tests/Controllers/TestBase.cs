using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alten.Connected_Vehicles.BLL.Interfaces;
using DryIoc;
using System.Data.Entity;
using Alten.Connected_vehicle.Model;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using Alten.Connected_Vehicles.Infrastructure.UnitOfWork;
using Alten.Connected_Vehicles.MSSQLRepository;
using Alten.Connected_Vehicles.BLL.Services;

namespace Alten.Connected_Vehicles.WebAPI.Tests.Controllers
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
            container.Register<IRawTransactionService, RawTransactionService>();
            container.Register<ITransactionService, TransactionService>();


            _customerService = container.Resolve<ICustomerService>();
            _vehicleService = container.Resolve<IVehicleService>();
            /////////////End of DI Configuration ////////////////////////

        }

    }
}
