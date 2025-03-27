using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Entity;
using MvcAspAzure.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class RouteController : BaseController<Cargo> {
        public RouteController(IRepositoryAsync<Cargo> repository) : base(repository) { }
    }
    //public sealed class RouteController : ControllerBase {
    //    readonly IRepository<Entity.Route> repository;

    //    public RouteController(IRepository<Entity.Route> repository) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Entity.Route route) {
    //        await repository.Insert(route);
    //        return Ok(route);
    //    }
    //}
}
