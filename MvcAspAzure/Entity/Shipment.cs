using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAspAzure.Entity {
    public sealed class Shipment {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartData { get; set; }

        [Required]
        public DateTime CompletionData { get; set; }

        [Required]
        public int RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        public Route Route { get; set; } = null!;

        [Required]
        public int DriverTruckId { get; set; }

        [ForeignKey(nameof(DriverTruckId))]
        public DriverTruck DriverTruck { get; set; } = null!;

        [Required]
        public int CargoId { get; set; }

        [ForeignKey(nameof(CargoId))]
        public Cargo Cargo { get; set; } = null!;
    }
}
