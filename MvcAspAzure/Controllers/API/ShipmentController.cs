using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Entity;
using MvcAspAzure.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ShipmentController : BaseController<Cargo> {
        public ShipmentController(IRepositoryAsync<Cargo> repository) : base(repository) { }
    }
    //public sealed class ShipmentController : ControllerBase {
    //    readonly IRepository<Shipment> repository;

    //    public ShipmentController(IRepository<Shipment> repository) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Shipment shipment) {
    //        await repository.Insert(shipment);
    //        return Ok(shipment);
    //    }
    //}
}
