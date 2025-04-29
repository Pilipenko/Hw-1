using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Handlers.Commands.Warehouse;
using MvcAspAzure.Application.Handlers.Queries.GetWarehouseByIdHandler;
using MvcAspAzure.Application.Handlers.Queries.WarehouseByIdQuery;
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class WarehouseController : ControllerBase {
        readonly WarehouseCommandHandler _handler;
        readonly GetWarehouseByIdHandler _queryHandler;

        public WarehouseController(WarehouseCommandHandler handler, GetWarehouseByIdHandler queryHandler) {
            _handler = handler;
            _queryHandler = queryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command) {
            var id = await _handler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWarehouseCommand command) {
            if (id != command.Id)
                return BadRequest();

            await _handler.Handle(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _handler.Handle(new DeleteWarehouseCommand { Id = id });
            return NoContent();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var shipment = await _queryHandler.Handle(new GetWarehouseByIdQuery(id));
            if (shipment == null)
                return NotFound();

            return Ok(shipment);
        }

    }
}
