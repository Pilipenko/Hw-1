using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Handlers.Commands.Shipment;
using MvcAspAzure.Application.Handlers.Queries.AllShipmentsHandler;
using MvcAspAzure.Application.Handlers.Queries.AllShipmentsQuery;
using MvcAspAzure.Application.Handlers.Queries.GetShipmentByIdHandler;
using MvcAspAzure.Application.Handlers.Queries.ShipmentByIdQuery;
using MvcAspAzure.Domain.Entity;

namespace MvcAspAzure.Controllers.API {

    [Route("api/[controller]")]
    [ApiController]
    public sealed class ShipmentController : ControllerBase {
        readonly ShipmentCommandHandler _handler;
        readonly GetShipmentByIdHandler _queryHandler;
        readonly GetAllShipmentsHandler _getAllHandler;

        public ShipmentController(ShipmentCommandHandler handler, 
            GetShipmentByIdHandler queryHandler,
            GetAllShipmentsHandler getAllHandler) {
            _handler = handler;
            _queryHandler = queryHandler;
            _getAllHandler = getAllHandler;
        }


        //Bearer eyJhbGciOiJIUzI1NiIsInR...
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentCommand command) {
            var id = await _handler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateShipmentCommand command) {
            if (id != command.Id)
                return BadRequest();

            await _handler.Handle(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await _handler.Handle(new DeleteShipmentCommand { Id = id });
            return NoContent();
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var shipment = await _queryHandler.Handle(new GetShipmentByIdQuery(id));
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