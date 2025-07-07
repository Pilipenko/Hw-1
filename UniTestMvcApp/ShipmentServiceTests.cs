using Moq;
using FluentValidation;
using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Domain.Repository;
using FluentAssertions;

public class ShipmentServiceTests {
    [Theory, AutoMoqData]
    public async Task CreateShipmentAsync_ShouldReturnSuccess_WhenValidationIsValid(
        CreateShipmentCommand shipment,
        ShipmentService sut,
        Mock<IValidator<CreateShipmentCommand>> shipmentValidatorMock) {
        shipmentValidatorMock.Setup(v => v.ValidateAsync(shipment, default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var result = await sut.CreateShipmentAsync(shipment);

        result.Success.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }

    [Theory, AutoMoqData]
    public async Task CreateShipmentAsync_ShouldReturnErrors_WhenValidationFails(
        CreateShipmentCommand shipment,
        ShipmentService sut,
        Mock<IValidator<CreateShipmentCommand>> shipmentValidatorMock) {
        var failures = new List<FluentValidation.Results.ValidationFailure>
        {
            new FluentValidation.Results.ValidationFailure("Property", "Error message")
        };
        var validationResult = new FluentValidation.Results.ValidationResult(failures);

        shipmentValidatorMock.Setup(v => v.ValidateAsync(shipment, default))
            .ReturnsAsync(validationResult);

        var result = await sut.CreateShipmentAsync(shipment);

        result.Success.Should().BeFalse();
        result.Errors.Should().Contain("Error message");
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ShouldReturnShipment_WhenFound(
        int id,
        MvcAspAzure.Domain.Entity.Shipment shipmentFromRepo,
        ShipmentService sut,
        Mock<IShipmentRepository> repositoryMock) {
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(shipmentFromRepo);

        var result = await sut.GetByIdAsync(id);

        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(shipmentFromRepo.Id);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ShouldReturnFail_WhenShipmentNotFound(
        int id,
        ShipmentService sut,
        Mock<IShipmentRepository> repositoryMock) {
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((MvcAspAzure.Domain.Entity.Shipment?)null);

        var result = await sut.GetByIdAsync(id);

        result.Success.Should().BeFalse();
        result.Errors.Should().Contain($"Shipment with ID {id} not found.");
    }
}

