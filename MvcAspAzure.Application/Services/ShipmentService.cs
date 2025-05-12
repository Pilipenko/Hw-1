
using FluentValidation;

using MvcAspAzure.Application.Common;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Services {
    public sealed class ShipmentService {
        readonly IValidator<CreateShipmentCommand> _shipmentValidator;
        readonly IShipmentRepository _repository;

        public ShipmentService(IValidator<CreateShipmentCommand> shipmentValidator, IShipmentRepository repository) {
            _shipmentValidator = shipmentValidator;
            _repository= repository;
        }

        public async Task<ServiceResult> CreateShipmentAsync(CreateShipmentCommand shipment) {
            var validationResult = await _shipmentValidator.ValidateAsync(shipment);

            if (!validationResult.IsValid) {
                return new ServiceResult {
                    Success = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult<Domain.Entity.Shipment>> GetByIdAsync(int id) {
            var shipment = await _repository.GetByIdAsync(id);

            if (shipment == null) {
                return ServiceResult<Domain.Entity.Shipment>.Fail($"Shipment with ID {id} not found.");
            }

            var shipmentDto = new Domain.Entity.Shipment {
                Id = shipment.Id,
                StartData = shipment.StartData,
                CompletionData = shipment.CompletionData,
                RouteId = shipment.RouteId,
                DriverTruckId = shipment.DriverTruckId,
                CargoId = shipment.CargoId
            };
            return ServiceResult<Domain.Entity.Shipment>.Ok(shipmentDto);
        }
    }
}