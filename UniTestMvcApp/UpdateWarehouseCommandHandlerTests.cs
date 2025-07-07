using AutoFixture.Xunit2;

using Moq;

using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;


public class UpdateWarehouseCommandHandlerTests {
    [Theory, AutoMoqData]
    public async Task Handle_WhenWarehouseExists_ShouldUpdateWarehouse(
        UpdateWarehouseCommand command,
        Warehouse existingWarehouse,
        [Frozen] Mock<IWarehouseRepository> warehouseRepositoryMock,
        UpdateWarehouseCommandHandler sut)
    {

        existingWarehouse.Id = command.Id;
        warehouseRepositoryMock.Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync(existingWarehouse);

        await sut.Handle(command);

        warehouseRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Warehouse>(w => w.PlaceId == command.PlaceId)), Times.Once);
    }
}

public class DeleteWarehouseCommandHandlerTests {
    [Theory, AutoMoqData]
    public async Task Handle_WhenWarehouseExists_ShouldDeleteWarehouse(
        DeleteWarehouseCommand command,
        Warehouse existingWarehouse,
        [Frozen] Mock<IWarehouseRepository> warehouseRepositoryMock,
        DeleteWarehouseCommandHandler sut) {

        existingWarehouse.Id = command.Id;
        warehouseRepositoryMock.Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync(existingWarehouse);

        await sut.Handle(command);

        warehouseRepositoryMock.Verify(x => x.DeleteAsync(command.Id), Times.Once);
    }
}

public class CreateWarehouseCommandHandlerTests {
    [Theory, AutoMoqData]
    public async Task Handle_ShouldInsertWarehouseAndReturnId(
        CreateWarehouseCommand command,
        Warehouse insertedWarehouse,
        [Frozen] Mock<IWarehouseRepository> warehouseRepositoryMock,
        CreateWarehouseCommandHandler sut) {

        insertedWarehouse.Id = command.Id;
        insertedWarehouse.PlaceId = command.PlaceId;
        warehouseRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<Warehouse>()))
            .ReturnsAsync(insertedWarehouse);

        var result = await sut.Handle(command);

        Assert.Equals(command.Id, result);
        warehouseRepositoryMock.Verify(x => x.InsertAsync(It.Is<Warehouse>(w => w.Id == command.Id && w.PlaceId == command.PlaceId)), Times.Once);
    }
}

