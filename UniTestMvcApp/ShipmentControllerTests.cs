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

[TestFixture]
public class ShipmentControllerTests {
    private IFixture fixture;

    private Mock<IShipmentOperations> shipmentOperationsMock;
    private Mock<ShipmentService> shipmentServiceMock;
    private Mock<IValidator<CreateShipmentCommand>> validatorMock;

    private ShipmentController controller;

    [SetUp]
    public void Setup() {
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

        Assert.IsInstanceOf<CreatedAtActionResult>(result);
        var createdResult = (CreatedAtActionResult)result;

        Assert.That(createdResult.ActionName, Is.EqualTo(nameof(ShipmentController.GetById)));
        Assert.That(createdResult.RouteValues["id"], Is.EqualTo(command.Id));
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

        Assert.IsInstanceOf<ObjectResult>(result);
        var objectResult = (ObjectResult)result;
        Assert.AreEqual(400, objectResult.StatusCode);

        var problemDetails = objectResult.Value as ValidationProblemDetails;
        Assert.IsNotNull(problemDetails);
        Assert.IsTrue(problemDetails.Errors.ContainsKey("Name"));
    }
}

public sealed class ShipmentController : ControllerBase {
    private readonly IShipmentOperations _shipmentOperations;
    private readonly ShipmentService _shipmentService;
    private readonly IValidator<CreateShipmentCommand> _validator;

    public ShipmentController(
        IShipmentOperations shipmentOperations,
        ShipmentService shipmentService,
        IValidator<CreateShipmentCommand> validator) {
        _shipmentOperations = shipmentOperations;
        _shipmentService = shipmentService;
        _validator = validator;
    }

    public async Task<IActionResult> Create(CreateShipmentCommand command) {
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

    public async Task<IActionResult> GetById(int id) {
        return Ok();
    }
}
