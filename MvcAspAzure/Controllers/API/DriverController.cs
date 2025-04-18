using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class DriverController : ControllerBase {
        readonly IRepository<Driver> repository;

        public DriverController(IRepository<Driver> repository) {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(Driver driver) {
            await repository.Insert(driver);
            return Ok(driver);
        }
    }
}
