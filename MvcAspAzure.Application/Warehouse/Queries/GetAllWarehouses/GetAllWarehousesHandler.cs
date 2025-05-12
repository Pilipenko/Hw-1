using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses {
    public sealed class GetAllWarehousesHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public GetAllWarehousesHandler(IWarehouseRepository warehouseRepository) {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<Domain.Entity.Warehouse>> Handle(GetAllWarehousesQuery query) {
            return await _warehouseRepository.GetAllAsync();
        }
    }
}
