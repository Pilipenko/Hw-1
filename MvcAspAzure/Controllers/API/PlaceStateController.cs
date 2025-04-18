using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class PlaceStateController : ControllerBase {
        readonly IRepository<PlaceState> repository;

        public PlaceStateController(IRepository<PlaceState> repository) {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(PlaceState placeState) {
            await repository.Insert(placeState);
            return Ok(placeState);
        }
    }
}
