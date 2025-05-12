using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Services;
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
        readonly WarehouseService _warehouseService;
        readonly IValidator<CreateWarehouseCommand> _validator;
        public WarehouseController(
            UpdateWarehouseCommandHandler updateHandler,
            CreateWarehouseCommandHandler createHandler,
            DeleteWarehouseCommandHandler deleteHandler,
            GetWarehouseByIdHandler getByIdHandler,
            GetAllWarehousesHandler getAllHandler,
            WarehouseService shipmentService,
            IValidator<CreateWarehouseCommand> validator) {
            _updatehandler = updateHandler;
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
            _warehouseService = shipmentService;
            _validator = validator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command) {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid) {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return ValidationProblem(ModelState);
            }

            var result = await _warehouseService.CreateWarehouseAsync(command);

            if (!result.Success)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetById), new { id = command.Id }, null);
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
            var shipment = await _warehouseService.GetByIdAsync(id);
            if (shipment == null)
                return NotFound();

            return Ok(shipment);

            //var warehouse = await _getByIdHandler.Handle(new GetWarehouseByIdQuery(id));
            //if (warehouse == null)
            //    return NotFound();

            //return Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var shipments = await _getAllHandler.Handle(new GetAllWarehousesQuery());
            return Ok(shipments);
        }

    }
}
