using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById {
    public sealed class GetWarehouseByIdHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public GetWarehouseByIdHandler(IWarehouseRepository shipmentRepository) {
            _warehouseRepository = shipmentRepository;
        }

        public async Task<Domain.Entity.Warehouse> Handle(GetWarehouseByIdQuery query) {
            if (query is null) {
                throw new ArgumentNullException(nameof(query));
            }

            return await _warehouseRepository.GetByIdAsync(query.Id);
        }
    }
}
