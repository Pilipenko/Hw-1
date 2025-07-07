using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using MvcAspAzure.Application.Common;
using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Services.Interfaces;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Controllers.API;


public class ShipmentControllerTests {
    private readonly IFixture fixture;
    private readonly ShipmentController controller;
    private readonly Mock<IShipmentOperations> shipmentOperationsMock;
    private readonly Mock<ShipmentService> shipmentServiceMock;
    private readonly Mock<IValidator<CreateShipmentCommand>> validatorMock;

    public ShipmentControllerTests() {
        fixture = new Fixture()
            .Customize(new AutoMoqCustomization { ConfigureMembers = true });

        var command = fixture.Create<CreateShipmentCommand>();

        shipmentOperationsMock = fixture.Freeze<Mock<IShipmentOperations>>();
        shipmentServiceMock = fixture.Freeze<Mock<ShipmentService>>();
        validatorMock = fixture.Freeze<Mock<IValidator<CreateShipmentCommand>>>();

        controller = fixture.Create<ShipmentController>();
    }

    [Theory, AutoData]
    public async Task Create_ValidCommand_ReturnsCreated() {
        var command = fixture.Create<CreateShipmentCommand>();

        validatorMock
            .Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult());

        shipmentServiceMock
            .Setup(s => s.CreateShipmentAsync(command))
            .ReturnsAsync(new ServiceResult { Success = true });

        // Act
        var result = await controller.Create(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
        var createdResult = (CreatedAtActionResult)result;
        Assert.That(createdResult.ActionName, Is.EqualTo(nameof(ShipmentController.GetById)));
        Assert.That(createdResult.RouteValues["id"], Is.EqualTo(command.Id));
    }

    [Theory, AutoData]
    public async Task Create_InvalidCommand_ReturnsValidationProblem() {
        var command = fixture.Create<CreateShipmentCommand>();

        var failures = new List<ValidationFailure>
        {
        new ValidationFailure("Name", "Name is required")
    };
        var validationResult = new FluentValidation.Results.ValidationResult(failures);

        validatorMock
            .Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = await controller.Create(command);

        Assert.IsInstanceOf<CreatedAtActionResult>(result);

        var validationProblemResult = (CreatedAtActionResult)result;
        Assert.That(validationProblemResult.StatusCode, Is.EqualTo(400));

        var modelState = validationProblemResult.Value as ValidationProblemDetails;
        Assert.IsNotNull(modelState);
        Assert.That(modelState.Errors.ContainsKey("Name"));
        Assert.That(modelState.Errors["Name"], Has.Member("Name is required"));
    }

}
