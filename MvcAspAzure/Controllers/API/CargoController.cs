using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class CargoController : ControllerBase {
        readonly IRepository<Cargo> repository;

        public CargoController(ShipmenDbContext context) {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(Cargo cargo) {
            await repository.Insert(cargo);
            return Ok(cargo);
        }
    }
}
