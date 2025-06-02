using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse {
    public sealed class UpdateWarehouseCommand {
        public required int Id { get; set; }
        public int PlaceId { get; set; }
    }
}
