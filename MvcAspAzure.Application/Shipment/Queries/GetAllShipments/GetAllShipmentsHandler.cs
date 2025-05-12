using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Shipment.Queries.GetAllShipments {
    public sealed class GetAllShipmentsHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public GetAllShipmentsHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<IEnumerable<Domain.Entity.Shipment>> Handle(GetAllShipmentsQuery query) {
            return await _shipmentRepository.GetAllAsync();
        }
    }
}
