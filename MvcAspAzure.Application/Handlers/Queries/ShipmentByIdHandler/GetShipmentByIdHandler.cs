using MvcAspAzure.Application.Handlers.Queries.ShipmentByIdQuery;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers.Queries.GetShipmentByIdHandler {
    public sealed class GetShipmentByIdHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public GetShipmentByIdHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Shipment> Handle(GetShipmentByIdQuery query) {
            if (query is null) {
                throw new ArgumentNullException(nameof(query));
            }

            return await _shipmentRepository.GetByIdAsync(query.Id);
        }
    }
}
