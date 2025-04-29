using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Entity;

using Route = MvcAspAzure.Domain.Entity.Route;

namespace MvcAspAzure.Domain.Data {
    public sealed class ShipmenDbContext: DbContext {
        public ShipmenDbContext(DbContextOptions<ShipmenDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<PlaceState> PlaceStates { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<DriverTruck> DriverTrucks { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Make Unique fields
            modelBuilder.Entity<Shipment>(static entity => {
                //entity.ToTable("Shipments");

                entity.HasKey(s => s.Id);
                entity.HasKey(s => s.RouteId);
                entity.HasKey(s => s.CargoId);


                //entity.HasOne(s => s.RouteId)
                //      .WithMany(w => w.OriginWarehouse)
                //      .HasForeignKey(s => s.RouteId);
            });

            modelBuilder.Entity<Warehouse>(entity => {
                //entity.ToTable("Warehouses");

                entity.HasKey(w => w.Id);
                entity.HasKey(w => w.PlaceId);
            });
        }

    }
}
