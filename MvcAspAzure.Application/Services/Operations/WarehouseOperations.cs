
using MvcAspAzure.Application.Services.Interfaces;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;
using MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses;
using MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById;

namespace MvcAspAzure.Application.Services.Operations {
    public sealed class WarehouseOperations : IWarehouseOperations {
        private readonly UpdateWarehouseCommandHandler _updateHandler;
        private readonly CreateWarehouseCommandHandler _createHandler;
        private readonly DeleteWarehouseCommandHandler _deleteHandler;
        private readonly GetWarehouseByIdHandler _getByIdHandler;
        private readonly GetAllWarehousesHandler _getAllHandler;

        public WarehouseOperations(
            UpdateWarehouseCommandHandler updateHandler,
            CreateWarehouseCommandHandler createHandler,
            DeleteWarehouseCommandHandler deleteHandler,
            GetWarehouseByIdHandler getByIdHandler,
            GetAllWarehousesHandler getAllHandler) {
            _updateHandler = updateHandler;
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        public async Task<int> CreateAsync(CreateWarehouseCommand command) {
            return await _createHandler.Handle(command);
        }

        public Task UpdateAsync(UpdateWarehouseCommand command) {
            return _updateHandler.Handle(command);
        }

        public Task DeleteAsync(int warehouseId) {
            return _deleteHandler.Handle(new DeleteWarehouseCommand { Id = warehouseId });
        }

        public async Task<Domain.Entity.Warehouse> GetByIdAsync(int warehouseId) {
            return await _getByIdHandler.Handle(new GetWarehouseByIdQuery(warehouseId));
        }

        public async Task<IEnumerable<Domain.Entity.Warehouse>> GetAllAsync() {
            return await _getAllHandler.Handle(new GetAllWarehousesQuery());
        }
    }
}
