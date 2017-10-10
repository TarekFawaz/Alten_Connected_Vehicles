namespace Alten.Connected_vehicle.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Common;

    public partial class Connected_Vehicles_Models : DbContext
    {
        public Connected_Vehicles_Models()
            : base("name=Connected_Vehicles_Models")
        {
        }

       
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RawTransction> RawTransctions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.RegNo)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.RegNo)
                .IsUnicode(false);

            Database.SetInitializer<Connected_Vehicles_Models>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
