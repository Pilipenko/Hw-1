using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Handlers.Commands.Warehouse;
using MvcAspAzure.Application.Handlers.Queries.AllWarehousesHandler;
using MvcAspAzure.Application.Handlers.Queries.AllWarehousesQuery;
using MvcAspAzure.Application.Handlers.Queries.GetWarehouseByIdHandler;
using MvcAspAzure.Application.Handlers.Queries.WarehouseByIdQuery;
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class WarehouseController : ControllerBase {
        readonly WarehouseCommandHandler _handler;
        readonly GetWarehouseByIdHandler _getByIdHandler;
        readonly GetAllWarehousesHandler _getAllHandler;

        public WarehouseController(WarehouseCommandHandler handler, 
            GetWarehouseByIdHandler getByIdHandler,
            GetAllWarehousesHandler getAllHandler) {
            _handler = handler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
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



        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var warehouse = await _getByIdHandler.Handle(new GetWarehouseByIdQuery(id));
            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var shipments = await _getAllHandler.Handle(new GetAllWarehousesQuery());
            return Ok(shipments);
        }

    }
}
