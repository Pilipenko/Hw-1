using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Domain.Repository {
    public interface IWarehouseRepository : IRepositoryAsync<Warehouse> {
        Task<IEnumerable<Warehouse>> GetWarehouseByPlaceIdAsync(int warehouseId);
    }
}

