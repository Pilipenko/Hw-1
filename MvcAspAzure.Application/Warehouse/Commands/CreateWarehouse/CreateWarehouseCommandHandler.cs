

using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse {
    public sealed class CreateWarehouseCommandHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public CreateWarehouseCommandHandler(IWarehouseRepository WarehouseRepository) {
            _warehouseRepository = WarehouseRepository;
        }

        public async Task<int> Handle(CreateWarehouseCommand command) {
            var warehouse = new Domain.Entity.Warehouse {
                Id = command.Id,
                PlaceId = command.PlaceId,
                Place = command.Place,
            };
            var createdWarehouse = await _warehouseRepository.InsertAsync(warehouse);
            return createdWarehouse.Id;
        }
    }
}
