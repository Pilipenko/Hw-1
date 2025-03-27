using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAspAzure.Entity {
    public sealed class Cargo {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal Volume { get; set; }

        [Required]
        public int RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        public Route Route { get; set; } = null!;

        [Required]
        public int SenderContactId { get; set; }

        [ForeignKey(nameof(SenderContactId))]
        public Contact SenderContact { get; set; } = null!;

        [Required]
        public int RecipientContactId { get; set; }

        [ForeignKey(nameof(RecipientContactId))]
        public Contact RecipientContact { get; set; } = null!;
    }
}
