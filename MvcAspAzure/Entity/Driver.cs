using System.ComponentModel.DataAnnotations;


namespace MvcAspAzure.Entity {
    public sealed class Driver {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        public ICollection<DriverTruck> DriverTrucks { get; set; } = new List<DriverTruck>();
    }
}
