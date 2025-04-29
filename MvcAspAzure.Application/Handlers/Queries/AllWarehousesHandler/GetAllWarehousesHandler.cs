
using MvcAspAzure.Application.Handlers.Queries.AllWarehousesQuery;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers.Queries.AllWarehousesHandler {
    public sealed class GetAllWarehousesHandler {
        private readonly IWarehouseRepository _warehouseRepository;

        public GetAllWarehousesHandler(IWarehouseRepository warehouseRepository) {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesQuery query) {
            return await _warehouseRepository.GetAllAsync();
        }
    }
}
