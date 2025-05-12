
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Shipment.Commands.DeleteShipment {
    public sealed class DeleteShipmentCommandHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public DeleteShipmentCommandHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task Handle(DeleteShipmentCommand command) {
            var shipment = await _shipmentRepository.GetByIdAsync(command.Id);
            if (shipment != null) {
                await _shipmentRepository.DeleteAsync(shipment.Id);
            }
        }
    }
}
