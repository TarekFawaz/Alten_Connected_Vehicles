namespace Alten.Connected_vehicle.Model
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Connected_Vehicles_Models : DbContext
    {
        public Connected_Vehicles_Models()
        {
        }

        public Connected_Vehicles_Models(DbContextOptions<Connected_Vehicles_Models> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RawTransction> RawTransctions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Connection string will be configured via DI or app settings
                // optionsBuilder.UseSqlServer("name=Connected_Vehicles_Models");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
