using System.ComponentModel.DataAnnotations;


namespace MvcAspAzure.Entity {
    public sealed  class State {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string StateName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string StateCode { get; set; } = string.Empty;

        public ICollection<PlaceState> Places { get; set; } = new List<PlaceState>();
    }
}
