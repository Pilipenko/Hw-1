using FluentValidation;

namespace MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse {
    public sealed class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand> {
        public CreateWarehouseCommandValidator() {
            RuleFor(w => w.PlaceId)
                .GreaterThan(0).WithMessage("PlaceId must be greater than 0.");

            RuleFor(w => w.Place)
                .NotNull().WithMessage("Place cannot be null.");
        }
    }
}
