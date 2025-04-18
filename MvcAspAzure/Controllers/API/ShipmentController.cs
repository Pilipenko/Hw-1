using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ShipmentController : ControllerBase {
        readonly IRepository<Shipment> repository;

        public ShipmentController(IRepository<Shipment> repository) {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(Shipment shipment) {
            await repository.Insert(shipment);
            return Ok(shipment);
        }
    }
}
