
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Shipment.Commands.UpdateShipment {
    public sealed class UpdateShipmentCommandHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public UpdateShipmentCommandHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task Handle(UpdateShipmentCommand command) {
            var shipment = await _shipmentRepository.GetByIdAsync(command.Id);
            if (shipment != null) {
                shipment.CompletionData = command.CompletionData;
                shipment.RouteId = command.RouteId;
                shipment.Cargo = command.Cargo;

                await _shipmentRepository.UpdateAsync(shipment);
            }
        }
    }
}
