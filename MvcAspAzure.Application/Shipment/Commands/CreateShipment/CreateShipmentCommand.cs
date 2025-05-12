using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Application.Shipment.Commands.CreateShipment {
    public sealed class CreateShipmentCommand {
        public required int Id { get; set; }
        public DateTime StartData { get; set; }
        public DateTime CompletionData { get; set; }
        public int RouteId { get; set; }
        public required Route Route { get; set; }
        public required Cargo Cargo { get; set; }
        public int CargoId { get; set; }
    }
}
