using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class WarehouseController : BaseController<Cargo> {
        public WarehouseController(IRepositoryAsync<Cargo> repository) : base(repository) { }
    }
    //public sealed class WarehouseController : ControllerBase {
    //    readonly IRepository<Warehouse> repository;

    //    public WarehouseController(IRepository<Warehouse> repository) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Warehouse warehouse) {
    //        await repository.Insert(warehouse);
    //        return Ok(warehouse);
    //    }
    //}
}
