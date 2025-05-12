using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse {
    public sealed class CreateWarehouseCommand {
        public required int Id { get; set; }
        public int PlaceId { get; set; }
        public required PlaceState Place { get; set; }
    }
}
