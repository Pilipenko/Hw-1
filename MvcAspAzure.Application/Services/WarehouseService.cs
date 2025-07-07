using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using MvcAspAzure.Application.Common;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Domain.Repository;

namespace MvcAspAzure.Application.Services {
    public interface IWarehouseService {
        Task<ServiceResult> CreateWarehouseAsync(CreateWarehouseCommand command);
        Task<ServiceResult<Domain.Entity.Warehouse>> GetByIdAsync(int id);
    }

    public sealed class WarehouseService : IWarehouseService {
        readonly IValidator<CreateWarehouseCommand> _warehouseValidator;
        readonly IWarehouseRepository _repository;

        public WarehouseService(IValidator<CreateWarehouseCommand> warehouseValidator, IWarehouseRepository repository) {
            _warehouseValidator = warehouseValidator;
            _repository = repository;
        }

        public async Task<ServiceResult> CreateWarehouseAsync(CreateWarehouseCommand warehouse) {
            var validationResult = await _warehouseValidator.ValidateAsync(warehouse);

            if (!validationResult.IsValid) {
                return new ServiceResult {
                    Success = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult<Domain.Entity.Warehouse>> GetByIdAsync(int id) {
            var warehouse = await _repository.GetByIdAsync(id);

            if (warehouse == null) {
                return ServiceResult<Domain.Entity.Warehouse>.Fail($"Warehouse with ID {id} not found.");
            }

            var warehouseDto = new Domain.Entity.Warehouse {
                Id = warehouse.Id,
                PlaceId = warehouse.PlaceId,
            };
            return ServiceResult<Domain.Entity.Warehouse>.Ok(warehouseDto);
        }
    }
}