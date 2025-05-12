using FluentValidation;

using MvcAspAzure.Application.Shipment.Commands.CreateShipment;

namespace MvcAspAzure.Application.Warehouse.Commands.CreateShipmentValidator {
    public sealed class CreateShipmentCommandValidator : AbstractValidator<CreateShipmentCommand> {
        public CreateShipmentCommandValidator() {
            RuleFor(x => x.StartData)
                .LessThan(x => x.CompletionData)
                .WithMessage("StartData must be earlier than CompletionData.");

            RuleFor(x => x.RouteId)
                .GreaterThan(0).WithMessage("RouteId is required.");

            RuleFor(x => x.CargoId)
                .GreaterThan(0).WithMessage("CargoId is required.");


            //RuleFor(s => s.StartData)
            //    .NotEmpty().WithMessage("StartData is required.")
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("StartData cannot be in the future.");

            //RuleFor(s => s.CompletionData)
            //    .NotEmpty().WithMessage("CompletionData is required.")
            //    .GreaterThan(s => s.StartData).WithMessage("CompletionData must be later than StartData.");

            //RuleFor(s => s.RouteId)
            //    .NotEmpty().WithMessage("RouteId is required.")
            //    .GreaterThan(0).WithMessage("RouteId must be greater than 0.");

            //RuleFor(s => s.DriverTruckId)
            //    .NotEmpty().WithMessage("DriverTruckId is required.")
            //    .GreaterThan(0).WithMessage("DriverTruckId must be greater than 0.");

            //RuleFor(s => s.CargoId)
            //    .NotEmpty().WithMessage("CargoId is required.")
            //    .GreaterThan(0).WithMessage("CargoId must be greater than 0.");

            //RuleFor(s => s.Route)
            //    .NotNull().WithMessage("Route cannot be null.");

            //RuleFor(s => s.DriverTruck)
            //    .NotNull().WithMessage("DriverTruck cannot be null.");

            //RuleFor(s => s.Cargo)
            //    .NotNull().WithMessage("Cargo cannot be null.");

            //RuleFor(s => s.RouteId)
            //    .MustAsync(async (routeId, cancellationToken) => await IsRouteIdUnique(routeId))
            //    .WithMessage("RouteId must be unique.");
        }

        //async Task<bool> IsRouteIdUnique(int routeId) {
        //    return await _dbContext.Routes.AnyAsync(r => r.Id == routeId);
        //}
    }

}
