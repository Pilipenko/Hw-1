using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Data;
using MvcAspAzure.Entity;
using MvcAspAzure.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class CargoController : BaseController<Cargo> {
        public CargoController(IRepositoryAsync<Cargo> repository) : base(repository) { }
    }
    //public sealed class CargoController: ControllerBase {
    //    readonly IRepository<Cargo> repository;

    //    public CargoController(ShipmenDbContext context) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Cargo cargo) {
    //        await repository.Insert(cargo);
    //        return Ok(cargo);
    //    }
    //}
}
