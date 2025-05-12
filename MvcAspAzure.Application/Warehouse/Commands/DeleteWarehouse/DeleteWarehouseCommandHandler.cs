using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse {
    public sealed class DeleteWarehouseCommandHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public DeleteWarehouseCommandHandler(IWarehouseRepository WarehouseRepository) {
            _warehouseRepository = WarehouseRepository;
        }

        public async Task Handle(DeleteWarehouseCommand command) {
            var warehouse = await _warehouseRepository.GetByIdAsync(command.Id);
            if (warehouse != null) {
                await _warehouseRepository.DeleteAsync(warehouse.Id);
            }
        }
    }
}