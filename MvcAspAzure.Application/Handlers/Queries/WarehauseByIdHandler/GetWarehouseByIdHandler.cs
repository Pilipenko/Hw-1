using MvcAspAzure.Application.Handlers.Queries.WarehouseByIdQuery;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers.Queries.GetWarehouseByIdHandler {
    public sealed class GetWarehouseByIdHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public GetWarehouseByIdHandler(IWarehouseRepository shipmentRepository) {
            _warehouseRepository = shipmentRepository;
        }

        public async Task<Warehouse> Handle(GetWarehouseByIdQuery query) {
            if (query is null) {
                throw new ArgumentNullException(nameof(query));
            }

            return await _warehouseRepository.GetByIdAsync(query.Id);
        }
    }
}
