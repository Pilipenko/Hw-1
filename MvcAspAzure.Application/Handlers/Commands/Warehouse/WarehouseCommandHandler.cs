using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers.Commands.Warehouse {
    public sealed class WarehouseCommandHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseCommandHandler(IWarehouseRepository WarehouseRepository) {
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

        public async Task Handle(UpdateWarehouseCommand command) {
            var warehouse = await _warehouseRepository.GetByIdAsync(command.Id);
            if (warehouse != null) {
                warehouse.Place = command.Place;
                warehouse.PlaceId = command.PlaceId;

                await _warehouseRepository.UpdateAsync(warehouse);
            }
        }

        public async Task Handle(DeleteWarehouseCommand command) {
            var warehouse = await _warehouseRepository.GetByIdAsync(command.Id);
            if (warehouse != null) {
                await _warehouseRepository.DeleteAsync(warehouse.Id);
            }
        }
    }
}