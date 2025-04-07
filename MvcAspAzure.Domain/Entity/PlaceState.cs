using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAspAzure.Domain.Entity {
    public sealed class PlaceState {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string PlaceName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string PlaceCode { get; set; } = string.Empty;

        [Required]
        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; } = null!;

        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    }
}
