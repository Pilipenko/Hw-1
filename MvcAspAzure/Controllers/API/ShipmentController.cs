using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Application.Shipment.Commands.DeleteShipment;
using MvcAspAzure.Application.Shipment.Commands.UpdateShipment;
using MvcAspAzure.Application.Shipment.Queries.GetAllShipments;
using MvcAspAzure.Application.Shipment.Queries.GetShipmentById;
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ShipmentController : ControllerBase {
        readonly UpdateShipmentCommandHandler _updateHandler;
        readonly CreateShipmentCommandHandler _createHandler;
        readonly DeleteShipmentCommandHandler _deleteHandler;
        readonly GetShipmentByIdHandler _getByIdHandler;
        readonly GetAllShipmentsHandler _getAllHandler;
        readonly ShipmentService _shipmentService;
        readonly IValidator<CreateShipmentCommand> _validator;

        public ShipmentController(
            UpdateShipmentCommandHandler updateHandler,
            CreateShipmentCommandHandler createHandler,
            DeleteShipmentCommandHandler deleteHandler,
            GetShipmentByIdHandler getByIdHandler,
            GetAllShipmentsHandler getAllHandler,
            ShipmentService shipmentService,
            IValidator<CreateShipmentCommand> validator) {
            _updateHandler = updateHandler;
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
            _shipmentService= shipmentService;
            _validator = validator;
        }


        //Bearer eyJhbGciOiJIUzI1NiIsInR...
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentCommand command) {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid) {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return ValidationProblem(ModelState);
            }

            var result = await _shipmentService.CreateShipmentAsync(command);

            if (!result.Success)
                return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetById), new { id = command.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateShipmentCommand command) {
            if (id != command.Id)
                return BadRequest();

            await _updateHandler.Handle(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _deleteHandler.Handle(new DeleteShipmentCommand { Id = id });
            return NoContent();
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var shipment = await _shipmentService.GetByIdAsync(id);
            if (shipment == null)
                return NotFound();

            return Ok(shipment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var shipments = await _getAllHandler.Handle(new GetAllShipmentsQuery());
            return Ok(shipments);
        }
    }
}