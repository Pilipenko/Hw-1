using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Infrastructure.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class StateController : BaseController<Cargo> {
        public StateController(IRepositoryAsync<Cargo> repository) : base(repository) { }
    }
    //public sealed class StateController : ControllerBase {
    //    readonly IRepository<State> repository;

    //    public StateController(IRepository<State> repository) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(State state) {
    //        await repository.Insert(state);
    //        return Ok(state);
    //    }
    //}
}
