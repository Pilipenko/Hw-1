using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAspAzure.Entity {
    public sealed class DriverTruck {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TruckId { get; set; }

        [ForeignKey(nameof(TruckId))]
        public Truck Truck { get; set; } = null!;

        [Required]
        public int DriverId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public Driver Driver { get; set; } = null!;
    }
}
