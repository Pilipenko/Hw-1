using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class RouteController : ControllerBase {
        readonly IRepository<Entity.Route> repository;

        public RouteController(IRepository<Entity.Route> repository) {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(Entity.Route route) {
            await repository.Insert(route);
            return Ok(route);
        }
    }
}
