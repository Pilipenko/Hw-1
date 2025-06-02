using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Entity;

using Route = MvcAspAzure.Domain.Entity.Route;

namespace MvcAspAzure.Domain.Data {
    public sealed class ShipmenDbContext: DbContext {
        public ShipmenDbContext(DbContextOptions<ShipmenDbContext> options) : base(options) { }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<PlaceState> PlaceState { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Truck> Truck { get; set; }
        public DbSet<DriverTruck> DriverTruck { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Cargo> Cargo { get; set; }

    
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
