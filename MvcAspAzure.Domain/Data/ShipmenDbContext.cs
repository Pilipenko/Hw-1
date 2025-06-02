using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Entity;

using Route = MvcAspAzure.Domain.Entity.Route;

namespace MvcAspAzure.Domain.Data {
    public sealed class ShipmenDbContext: DbContext {
        public ShipmenDbContext(DbContextOptions<ShipmenDbContext> options) : base(options) { }

        //public DbSet<Contact> Contact { get; set; }
        //public DbSet<State> State { get; set; }
        //public DbSet<PlaceState> PlaceState { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        //public DbSet<Driver> Driver { get; set; }
        //public DbSet<Truck> Truck { get; set; }
        //public DbSet<DriverTruck> DriverTruck { get; set; }
        //public DbSet<Route> Route { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        //public DbSet<Cargo> Cargo { get; set; }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Make Unique fields
            modelBuilder.Entity<Shipment>(static entity => {
                entity.HasKey(s => s.Id);

                entity.HasOne(s => s.Route)
                      .WithMany()
                      .HasForeignKey(s => s.RouteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Warehouse>(entity => {
                entity.HasKey(w => w.Id);
            });
        }

    }
}
