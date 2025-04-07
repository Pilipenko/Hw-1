using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcAspAzure.Domain.Entity {
    public sealed class Warehouse {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlaceId { get; set; }

        [ForeignKey(nameof(PlaceId))]
        public PlaceState Place { get; set; } = null!;
    }
}
