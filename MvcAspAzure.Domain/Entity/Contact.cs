using System.ComponentModel.DataAnnotations;


namespace MvcAspAzure.Domain.Entity {
    public sealed class Contact {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string CellPhone { get; set; } = string.Empty;
    }
}
