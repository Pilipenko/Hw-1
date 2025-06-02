using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAspAzure.Domain.Entity {
    public sealed class Truck {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Brand { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public int Payload { get; set; }

        [Required, Column(TypeName = "decimal(5,2)")]
        public decimal FuelConsumption { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal VolumeCargo { get; set; }

        //public ICollection<DriverTruck> DriverTrucks { get; set; } = new List<DriverTruck>();
    }
}
