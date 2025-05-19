
using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Infrastructure.Repository;

namespace MvcAspAzure.Domain.Repository {
    public sealed class WarehouseRepository : RepositoryAsync<Warehouse>, IWarehouseRepository {
        private readonly ShipmenDbContext _context;

        public WarehouseRepository(ShipmenDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouseByPlaceIdAsync(int warehouseId) {
            return await _context.Warehouses
                .Where(w => w.PlaceId == warehouseId)
                .ToListAsync();
        }
    }
}
