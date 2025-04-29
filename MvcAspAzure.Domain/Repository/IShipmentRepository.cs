
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Domain.Repository {
    public interface IShipmentRepository : IRepositoryAsync<Shipment> {
        Task<IEnumerable<Shipment>> GetShipmentsByRouteIdAsync(int warehouseId);
        Task<IEnumerable<Shipment>> GetShipmentsByCargoIdAsync(int warehouseId);
    }
}
