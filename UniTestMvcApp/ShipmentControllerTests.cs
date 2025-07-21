using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;
using Xunit;
using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Services.Interfaces;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Controllers.API;
using MvcAspAzure.Application.Common;

public class ShipmentControllerTests {
    readonly IFixture fixture;
    readonly Mock<IShipmentOperations> shipmentOperationsMock;
    readonly Mock<ShipmentService> shipmentServiceMock;
    readonly Mock<IValidator<CreateShipmentCommand>> validatorMock;
    readonly ShipmentController controller;

    public ShipmentControllerTests() {
        fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

        shipmentOperationsMock = fixture.Freeze<Mock<IShipmentOperations>>();
        shipmentServiceMock = fixture.Freeze<Mock<ShipmentService>>();
        validatorMock = fixture.Freeze<Mock<IValidator<CreateShipmentCommand>>>();

        controller = new ShipmentController(
            shipmentOperationsMock.Object,
            shipmentServiceMock.Object,
            validatorMock.Object);
    }

    [Theory, AutoData]
    public async Task Create_ValidCommand_ReturnsCreated(CreateShipmentCommand command) {
        validatorMock
            .Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult());

        shipmentServiceMock
            .Setup(s => s.CreateShipmentAsync(command))
            .ReturnsAsync(new ServiceResult { Success = true });

        var result = await controller.Create(command);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ShipmentController.GetById), createdResult.ActionName);
        Assert.Equal(command.Id, createdResult.RouteValues["id"]);
    }

    [Theory, AutoData]
    public async Task Create_InvalidCommand_ReturnsValidationProblem(CreateShipmentCommand command) {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        };
        var validationResult = new ValidationResult(failures);

        validatorMock
            .Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = await controller.Create(command);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, objectResult.StatusCode);

        var problemDetails = Assert.IsType<ValidationProblemDetails>(objectResult.Value);
        Assert.True(problemDetails.Errors.ContainsKey("Name"));
    }
}

