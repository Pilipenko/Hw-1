
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Application.Handlers.Commands.Warehouse {
    public sealed class UpdateWarehouseCommand {
        public required int Id { get; set; }
        public required PlaceState Place { get; set; }
        public int PlaceId { get; set; }
    }
}
