using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Infrastructure.Repository;

namespace MvcAspAzure.Domain.Repository {
    public sealed class ShipmentRepository : RepositoryAsync<Shipment>, IShipmentRepository {
        private readonly ShipmenDbContext _context;

        public ShipmentRepository(ShipmenDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByRouteIdAsync(int warehouseId) {
            return await _context.Shipments
                .Where(s => s.RouteId == warehouseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByCargoIdAsync(int warehouseId) {
            return await _context.Shipments
                .Where(s => s.CargoId == warehouseId)
                .ToListAsync();
        }
    }
}
