
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Shipment.Commands.CreateShipment {
    public sealed class CreateShipmentCommandHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public CreateShipmentCommandHandler(IShipmentRepository shipmentRepository) {
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
    }
}
