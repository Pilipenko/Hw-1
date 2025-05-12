using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Shipment.Queries.GetShipmentById {
    public sealed class GetShipmentByIdHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public GetShipmentByIdHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Domain.Entity.Shipment> Handle(GetShipmentByIdQuery query) {
            if (query is null) {
                throw new ArgumentNullException(nameof(query));
            }

            return await _shipmentRepository.GetByIdAsync(query.Id);
        }
    }
}
