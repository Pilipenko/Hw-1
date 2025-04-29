
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers.Commands.Shipment {
    public sealed class ShipmentCommandHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentCommandHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<int> Handle(CreateShipmentCommand command) {
            var shipment = new Domain.Entity.Shipment {
                Id = command.Id,
                StartData = command.StartData,
                CompletionData = command.CompletionData,
                RouteId = command.RouteId,
                Route = command.Route,
                Cargo = command.Cargo,
                CargoId = command.CargoId,
            };
            var createdShipment = await _shipmentRepository.InsertAsync(shipment);
            return createdShipment.Id;
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

        public async Task Handle(DeleteShipmentCommand command) {
            var shipment = await _shipmentRepository.GetByIdAsync(command.Id);
            if (shipment != null) {
                await _shipmentRepository.DeleteAsync(shipment.Id);
            }
        }
    }
}
