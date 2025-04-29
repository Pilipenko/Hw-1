
namespace MvcAspAzure.Application.Handlers.Queries.ShipmentByIdQuery {
    public sealed class GetShipmentByIdQuery {
        public int Id { get; set; }

        public GetShipmentByIdQuery(int id) {
            Id = id;
        }
    }
}
