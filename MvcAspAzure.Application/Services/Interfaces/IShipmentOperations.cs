
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Application.Shipment.Commands.UpdateShipment;

namespace MvcAspAzure.Application.Services.Interfaces {
    public interface IShipmentOperations {
        Task<int> CreateAsync(CreateShipmentCommand command);
        Task UpdateAsync(UpdateShipmentCommand command);
        Task DeleteAsync(int shipmentId);
        Task<Domain.Entity.Shipment> GetByIdAsync(int shipmentId);
        Task<IEnumerable<Domain.Entity.Shipment>> GetAllAsync();
    }
}
