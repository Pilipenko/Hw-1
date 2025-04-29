
namespace MvcAspAzure.Application.Handlers.Queries.WarehouseByIdQuery {
    public sealed class GetWarehouseByIdQuery {
        public int Id { get; set; }

        public GetWarehouseByIdQuery(int id) {
            Id = id;
        }
    }
}
