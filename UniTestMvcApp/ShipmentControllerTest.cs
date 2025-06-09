using Xunit;
using Moq;
using FluentValidation;
using FluentValidation.Results;
using MvcAspAzure.Controllers.API;
using Microsoft.AspNetCore.Mvc;
using MvcAspAzure.Domain.Entity;

namespace UniTestMvcApp {


    public class ShipmentControllerTests {
        private readonly Mock<IShipmentOperations> _shipmentOps = new();
        private readonly Mock<ShipmentService> _shipmentService = new();
        private readonly Mock<IValidator<CreateShipmentCommand>> _validator = new();

        private ShipmentController CreateController() =>
            new ShipmentController(_shipmentOps.Object, _shipmentService.Object, _validator.Object);

        [Fact]
        public async Task Create_InvalidCommand_ReturnsValidationProblem() {
            var command = new CreateShipmentCommand();

            _validator.Setup(v => v.ValidateAsync(command, default))
                      .ReturnsAsync(new ValidationResult(new[] {
                      new ValidationFailure("SomeProp", "Some error")
                      }));

            var controller = CreateController();

            var result = await controller.Create(command);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equals(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task Create_ValidCommand_ReturnsCreatedAt() {
            var command = new CreateShipmentCommand { Id = 42 };

            _validator.Setup(v => v.ValidateAsync(command, default))
                      .ReturnsAsync(new ValidationResult());

            _shipmentService.Setup(s => s.CreateShipmentAsync(command))
                            .ReturnsAsync(new ResultDto { Success = true });

            var controller = CreateController();

            var result = await controller.Create(command);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equals(nameof(controller.GetById), createdAt.ActionName);
            Assert.Equals(42, createdAt.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_MismatchedIds_ReturnsBadRequest() {
            var command = new UpdateShipmentCommand { Id = 2 };
            var controller = CreateController();

            var result = await controller.Update(1, command);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ValidRequest_CallsUpdate_ReturnsNoContent() {
            var command = new UpdateShipmentCommand { Id = 1 };

            _shipmentOps.Setup(x => x.UpdateAsync(command))
                        .Returns(Task.CompletedTask);

            var controller = CreateController();

            var result = await controller.Update(1, command);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsNoContent() {
            _shipmentOps.Setup(x => x.DeleteAsync(1))
                        .Returns(Task.CompletedTask);

            var controller = CreateController();

            var result = await controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetById_Found_ReturnsOk() {
            var shipment = new Shipment { Id = 1 };

            _shipmentService.Setup(s => s.GetByIdAsync(1))
                            .ReturnsAsync(shipment);

            var controller = CreateController();

            var result = await controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equals(shipment, ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound() {
            _shipmentService.Setup(s => s.GetByIdAsync(99))
                            .ReturnsAsync(null as Shipment);

            var controller = CreateController();

            var result = await controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsList() {
            var shipments = new List<Shipment>
            {
            new Shipment { Id = 1 },
            new Shipment { Id = 2 }
        };

            _shipmentOps.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(shipments);

            var controller = CreateController();

            var result = await controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equals(shipments, ok.Value);
        }
    }
}