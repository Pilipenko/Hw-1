using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;
using MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses;
using MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById;
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class WarehouseController : ControllerBase {
        readonly UpdateWarehouseCommandHandler _updatehandler;
        readonly CreateWarehouseCommandHandler _createHandler;
        readonly DeleteWarehouseCommandHandler _deleteHandler;
        readonly GetWarehouseByIdHandler _getByIdHandler;
        readonly GetAllWarehousesHandler _getAllHandler;

        public WarehouseController(
            UpdateWarehouseCommandHandler updateHandler,
            CreateWarehouseCommandHandler createHandler,
            DeleteWarehouseCommandHandler deleteHandler,
            GetWarehouseByIdHandler getByIdHandler,
            GetAllWarehousesHandler getAllHandler) {
            _updatehandler = updateHandler;
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command) {
            var id = await _createHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWarehouseCommand command) {
            if (id != command.Id)
                return BadRequest();

            await _updatehandler.Handle(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _deleteHandler.Handle(new DeleteWarehouseCommand { Id = id });
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
