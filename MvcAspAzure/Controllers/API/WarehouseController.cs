using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Services.Interfaces;
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
        readonly IWarehouseOperations _warehouseOperations;
        readonly WarehouseService _warehouseService;
        readonly IValidator<CreateWarehouseCommand> _validator;
        public WarehouseController(
            IWarehouseOperations warehouseOperations,
            WarehouseService warehouseService,
            IValidator<CreateWarehouseCommand> validator) {
            _warehouseOperations = warehouseOperations;
            _warehouseService = warehouseService;
            _validator = validator;
        }

        //[Authorize]
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

            await _warehouseOperations.UpdateAsync(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _warehouseOperations.DeleteAsync(id);
            return NoContent();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var warehouse = await _warehouseService.GetByIdAsync(id);
            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var warehouses = await _warehouseOperations.GetAllAsync();
            return Ok(warehouses);
        }

    }
}
