

using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse {
    public sealed class UpdateWarehouseCommandHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public UpdateWarehouseCommandHandler(IWarehouseRepository WarehouseRepository) {
            _warehouseRepository = WarehouseRepository;
        }

        public async Task Handle(UpdateWarehouseCommand command) {
            var warehouse = await _warehouseRepository.GetByIdAsync(command.Id);
            if (warehouse != null) {
                warehouse.Place = command.Place;
                warehouse.PlaceId = command.PlaceId;

                await _warehouseRepository.UpdateAsync(warehouse);
            }
        }

    }
}
