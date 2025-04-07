using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Entity;

using Route = MvcAspAzure.Domain.Entity.Route;

namespace MvcAspAzure.Infrastructure.Data {
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
            modelBuilder.Entity<Truck>()
                .HasIndex(t => t.RegistrationNumber)
                .IsUnique();

            modelBuilder.Entity<DriverTruck>()
                .HasIndex(dt => new { dt.DriverId, dt.TruckId })
                .IsUnique();

            modelBuilder.Entity<Route>()
                        .ToTable(t => t.HasCheckConstraint("chk_Route_DifferentWarehouses", "OriginWarehouseId <> DestinationWarehouseId"));

            modelBuilder.Entity<Cargo>()
                        .ToTable(t => t.HasCheckConstraint("chk_Cargo_DifferentContact", "SenderContactId <> RecipientContactId"));
        }

    }
}
