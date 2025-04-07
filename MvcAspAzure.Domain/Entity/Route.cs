using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAspAzure.Domain.Entity {
    public sealed class Route {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Distance { get; set; }

        [Required]
        public int OriginWarehouseId { get; set; }

        [ForeignKey(nameof(OriginWarehouseId))]
        public Warehouse OriginWarehouse { get; set; } = null!;

        [Required]
        public int DestinationWarehouseId { get; set; }

        [ForeignKey(nameof(DestinationWarehouseId))]
        public Warehouse DestinationWarehouse { get; set; } = null!;
    }
}
