namespace Alten.Connected_vehicle.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Alten.Connected_vehicle.Model;
    internal sealed class Configuration : DbMigrationsConfiguration<Alten.Connected_vehicle.Model.Connected_Vehicles_Models>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Alten.Connected_vehicle.Model.Connected_Vehicles_Models context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Customers.AddOrUpdate(c => c.ID,
                new Customer { ID = Guid.Parse("EFB499FA-B179-4B99-9539-6925751F1FB6"), Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje", Active = true, CreatedOn = DateTime.Now, Deleted = false },
                new Customer { ID = Guid.Parse("A0860071-B1B8-4663-AAD6-6D75A6C92D47"), Name = "Johans Bulk AB", Address = "Balkvägen 12, 222 22 Stockholm", Active = true, CreatedOn = DateTime.Now, Deleted = false },
                new Customer { ID = Guid.Parse("D47CEC83-BCE8-46ED-B77D-D33D457319F7"), Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1, 333 33 Uppsala", Active = true, CreatedOn = DateTime.Now, Deleted = false }
                );

            context.Vehicles.AddOrUpdate(v => v.ID,
                new Vehicle {ID= "YS2R4X20005399401",RegNo= "ABC123",CustomerID=Guid.Parse("EFB499FA-B179-4B99-9539-6925751F1FB6"),ActiveStatus=false,Active=true,Deleted=false,LastUpdateTime=DateTime.Now,CreatedOn=DateTime.Now },
                new Vehicle { ID = "VLUR4X20009093588", RegNo = "DEF456", CustomerID = Guid.Parse("EFB499FA-B179-4B99-9539-6925751F1FB6"), ActiveStatus = false, Active = true, Deleted = false, LastUpdateTime = DateTime.Now, CreatedOn = DateTime.Now },
                new Vehicle { ID = "VLUR4X20009048066", RegNo = "GHI789", CustomerID = Guid.Parse("EFB499FA-B179-4B99-9539-6925751F1FB6"), ActiveStatus = false, Active = true, Deleted = false, LastUpdateTime = DateTime.Now, CreatedOn = DateTime.Now },

                new Vehicle { ID = "YS2R4X20005388011", RegNo = "JKL012", CustomerID = Guid.Parse("A0860071-B1B8-4663-AAD6-6D75A6C92D47"), ActiveStatus = false, Active = true, Deleted = false, LastUpdateTime = DateTime.Now, CreatedOn = DateTime.Now },
                new Vehicle { ID = "YS2R4X20005387949", RegNo = "MNO345", CustomerID = Guid.Parse("A0860071-B1B8-4663-AAD6-6D75A6C92D47"), ActiveStatus = false, Active = true, Deleted = false, LastUpdateTime = DateTime.Now, CreatedOn = DateTime.Now },

                new Vehicle { ID = "YS2R4X20005387765", RegNo = "PQR678", CustomerID = Guid.Parse("D47CEC83-BCE8-46ED-B77D-D33D457319F7"), ActiveStatus = false, Active = true, Deleted = false, LastUpdateTime = DateTime.Now, CreatedOn = DateTime.Now },
                new Vehicle { ID = "YS2R4X20005387055", RegNo = "STU901", CustomerID = Guid.Parse("D47CEC83-BCE8-46ED-B77D-D33D457319F7"), ActiveStatus = false, Active = true, Deleted = false, LastUpdateTime = DateTime.Now, CreatedOn = DateTime.Now }
                );

            
        }
    }
}
