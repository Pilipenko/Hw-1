using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Entity;
using MvcAspAzure.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class TruckController : BaseController<Cargo> {
        public TruckController(IRepositoryAsync<Cargo> repository) : base(repository) { }
    }
    //public sealed class TruckController : ControllerBase {
    //    readonly IRepository<Truck> repository;

    //    public TruckController(IRepository<Truck> repository) {
    //        this.repository = repository;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

    //    [HttpPost]
    //    public async Task<IActionResult> Create(Truck truck) {
    //        await repository.Insert(truck);
    //        return Ok(truck);
    //    }
    //}
}
