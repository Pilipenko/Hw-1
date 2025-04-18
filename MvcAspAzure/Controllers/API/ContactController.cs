using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ContactController : ControllerBase {
        readonly IRepository<Contact> repository;

        public ContactController(IRepository<Contact> repository) {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact) {
            await repository.Insert(contact);
            return Ok(contact);
        }
    }
}
