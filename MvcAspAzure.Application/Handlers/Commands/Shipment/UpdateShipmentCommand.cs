
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Application.Handlers.Commands.Shipment {
    public sealed class UpdateShipmentCommand {
        public required int Id { get; set; }
        public DateTime CompletionData { get; set; }
        public int RouteId { get; set; }
        public required Cargo Cargo { get; set; }
    }
}
