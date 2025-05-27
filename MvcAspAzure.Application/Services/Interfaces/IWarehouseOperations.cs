
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;

namespace MvcAspAzure.Application.Services.Interfaces {
    public interface IWarehouseOperations {
        Task<int> CreateAsync(CreateWarehouseCommand command);
        Task UpdateAsync(UpdateWarehouseCommand command);
        Task DeleteAsync(int id);
        Task<Domain.Entity.Warehouse> GetByIdAsync(int id);
        Task<IEnumerable<Domain.Entity.Warehouse>> GetAllAsync();
    }
}
