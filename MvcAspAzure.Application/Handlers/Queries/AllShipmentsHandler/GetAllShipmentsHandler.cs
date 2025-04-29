

using MvcAspAzure.Application.Handlers.Queries.AllShipmentsQuery;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Handlers.Queries.AllShipmentsHandler {
    public sealed class GetAllShipmentsHandler {
        private readonly IShipmentRepository _shipmentRepository;

        public GetAllShipmentsHandler(IShipmentRepository shipmentRepository) {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<IEnumerable<Shipment>> Handle(GetAllShipmentsQuery query) {
            return await _shipmentRepository.GetAllAsync();
        }
    }
}
