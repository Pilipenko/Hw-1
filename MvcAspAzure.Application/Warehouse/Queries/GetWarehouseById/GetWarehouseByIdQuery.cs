namespace MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById {
    public sealed class GetWarehouseByIdQuery {
        public int Id { get; set; }

        public GetWarehouseByIdQuery(int id) {
            Id = id;
        }
    }
}
