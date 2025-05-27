using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Application.Services.Interfaces;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Application.Shipment.Commands.DeleteShipment;
using MvcAspAzure.Application.Shipment.Commands.UpdateShipment;
using MvcAspAzure.Application.Shipment.Queries.GetAllShipments;
using MvcAspAzure.Application.Shipment.Queries.GetShipmentById;

namespace MvcAspAzure.Application.Services.Operations {
    public sealed class ShipmentOperations : IShipmentOperations {
        private readonly UpdateShipmentCommandHandler _updateHandler;
        private readonly CreateShipmentCommandHandler _createHandler;
        private readonly DeleteShipmentCommandHandler _deleteHandler;
        private readonly GetShipmentByIdHandler _getByIdHandler;
        private readonly GetAllShipmentsHandler _getAllHandler;

        public ShipmentOperations(
            UpdateShipmentCommandHandler updateHandler,
            CreateShipmentCommandHandler createHandler,
            DeleteShipmentCommandHandler deleteHandler,
            GetShipmentByIdHandler getByIdHandler,
            GetAllShipmentsHandler getAllHandler) {
            _updateHandler = updateHandler;
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        public async Task<int> CreateAsync(CreateShipmentCommand command) {
            return await _createHandler.Handle(command);
        }

        public Task UpdateAsync(UpdateShipmentCommand command) {
            return _updateHandler.Handle(command);
        }

        public Task DeleteAsync(int shipmentId) {
            return _deleteHandler.Handle(new DeleteShipmentCommand { Id = shipmentId });
        }

        public async Task<Domain.Entity.Shipment> GetByIdAsync(int shipmentId) {
            return await _getByIdHandler.Handle(new GetShipmentByIdQuery(shipmentId));
        }

        public async Task<IEnumerable<Domain.Entity.Shipment>> GetAllAsync() {
            return await _getAllHandler.Handle(new GetAllShipmentsQuery());
        }
    }

}
