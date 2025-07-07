using AutoFixture.Xunit2;

using FluentValidation;

using Moq;

using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Domain.Repository;

public class WarehouseServiceTests {
    [Theory, AutoMoqData]
    public async Task CreateWarehouseAsync_ShouldReturnSuccess_WhenValidationPasses(
        [Frozen] Mock<IValidator<CreateWarehouseCommand>> validatorMock,
        WarehouseService sut,
        CreateWarehouseCommand command) {
        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var result = await sut.CreateWarehouseAsync(command);

        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Errors);
    }

    [Theory, AutoMoqData]
    public async Task CreateWarehouseAsync_ShouldReturnErrors_WhenValidationFails(
        [Frozen] Mock<IValidator<CreateWarehouseCommand>> validatorMock,
        WarehouseService sut,
        CreateWarehouseCommand command) {
        var failures = new List<FluentValidation.Results.ValidationFailure>
        {
            new FluentValidation.Results.ValidationFailure("Name", "Name is required")
        };
        var validationResult = new FluentValidation.Results.ValidationResult(failures);

        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = await sut.CreateWarehouseAsync(command);

        Assert.IsFalse(result.Success);
        Assert.Contains("Name is required", result.Errors);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ShouldReturnWarehouse_WhenFound(
        [Frozen] Mock<IWarehouseRepository> repositoryMock,
        WarehouseService sut,
        MvcAspAzure.Domain.Entity.Warehouse warehouse) {
        repositoryMock.Setup(r => r.GetByIdAsync(warehouse.Id))
            .ReturnsAsync(warehouse);

        var result = await sut.GetByIdAsync(warehouse.Id);

        Assert.IsTrue(result.Success);
        Assert.Equals(warehouse.Id, b: result.Data.Id);
        Assert.Equals(warehouse.PlaceId, result.Data.PlaceId);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ShouldReturnFail_WhenNotFound(
        [Frozen] Mock<IWarehouseRepository> repositoryMock,
        WarehouseService sut,
        int id) {
        _ = repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(value: null);

        var result = await sut.GetByIdAsync(id);

        Assert.IsFalse(result.Success);
        Assert.Contains($"Warehouse with ID {id} not found.", result.Errors);
    }
}
