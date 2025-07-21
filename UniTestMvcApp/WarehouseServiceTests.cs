using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Xunit;
using FluentValidation.Results;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;
using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;

public class WarehouseServiceTests {
    [Theory, AutoMoqData]
    public async Task CreateWarehouseAsync_ShouldReturnSuccess_WhenValidationPasses(
        [Frozen] Mock<IValidator<CreateWarehouseCommand>> validatorMock,
        WarehouseService sut,
        CreateWarehouseCommand command) {
        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult());

        var result = await sut.CreateWarehouseAsync(command);

        Assert.True(result.Success);
        Assert.Null(result.Errors);
    }

    [Theory, AutoMoqData]
    public async Task CreateWarehouseAsync_ShouldReturnErrors_WhenValidationFails(
        [Frozen] Mock<IValidator<CreateWarehouseCommand>> validatorMock,
        WarehouseService sut,
        CreateWarehouseCommand command) {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        };
        var validationResult = new ValidationResult(failures);

        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(validationResult);

        var result = await sut.CreateWarehouseAsync(command);

        Assert.False(result.Success);
        Assert.Contains("Name is required", result.Errors);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ShouldReturnWarehouse_WhenFound(
        [Frozen] Mock<IWarehouseRepository> repositoryMock,
        WarehouseService sut,
        Warehouse warehouse) {
        repositoryMock.Setup(r => r.GetByIdAsync(warehouse.Id))
            .ReturnsAsync(warehouse);

        var result = await sut.GetByIdAsync(warehouse.Id);

        Assert.True(result.Success);
        Assert.Equal(warehouse.Id, result.Data.Id);
        Assert.Equal(warehouse.PlaceId, result.Data.PlaceId);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ShouldReturnFail_WhenNotFound(
        [Frozen] Mock<IWarehouseRepository> repositoryMock,
        WarehouseService sut,
        int id) {
        repositoryMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Warehouse)null);

        var result = await sut.GetByIdAsync(id);

        Assert.False(result.Success);
        Assert.Contains($"Warehouse with ID {id} not found.", result.Errors);
    }
}
