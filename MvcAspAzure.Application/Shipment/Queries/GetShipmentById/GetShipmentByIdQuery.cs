namespace MvcAspAzure.Application.Shipment.Queries.GetShipmentById {
    public sealed class GetShipmentByIdQuery {
        public int Id { get; set; }

        public GetShipmentByIdQuery(int id) {
            Id = id;
        }
    }
}
