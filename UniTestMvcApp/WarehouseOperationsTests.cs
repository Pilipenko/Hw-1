using Moq;
using Xunit;
using FluentAssertions;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse;
using MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById;
using MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses;
using MvcAspAzure.Application.Services.Operations;
using AutoFixture.Xunit2;

public class WarehouseOperationsTests {

    [Theory, AutoMoqData]
    public async Task CreateAsync_Should_Call_CreateHandler_And_Return_Id(
        CreateWarehouseCommand command,
        int expectedId,
        [Frozen] Mock<CreateWarehouseCommandHandler> createHandlerMock,
        WarehouseOperations warehouseOperations) {

        createHandlerMock.Setup(x => x.Handle(command)).ReturnsAsync(expectedId);

        var result = await warehouseOperations.CreateAsync(command);

        result.Should().Be(expectedId);
        createHandlerMock.Verify(x => x.Handle(command), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task UpdateAsync_Should_Call_UpdateHandler(
        UpdateWarehouseCommand command,
        [Frozen] Mock<UpdateWarehouseCommandHandler> updateHandlerMock,
        WarehouseOperations warehouseOperations) {

        updateHandlerMock.Setup(x => x.Handle(command)).Returns(Task.CompletedTask);

        await warehouseOperations.UpdateAsync(command);

        updateHandlerMock.Verify(x => x.Handle(command), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task DeleteAsync_Should_Call_DeleteHandler_With_Correct_Id(
        int warehouseId,
        [Frozen] Mock<DeleteWarehouseCommandHandler> deleteHandlerMock,
        WarehouseOperations warehouseOperations) {

        deleteHandlerMock.Setup(x => x.Handle(It.Is<DeleteWarehouseCommand>(cmd => cmd.Id == warehouseId)))
                         .Returns(Task.CompletedTask);

        await warehouseOperations.DeleteAsync(warehouseId);

        deleteHandlerMock.Verify(x => x.Handle(It.Is<DeleteWarehouseCommand>(cmd => cmd.Id == warehouseId)), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_Should_Call_GetByIdHandler_And_Return_Warehouse(
        int warehouseId,
        MvcAspAzure.Domain.Entity.Warehouse warehouse,
        [Frozen] Mock<GetWarehouseByIdHandler> getByIdHandlerMock,
        WarehouseOperations warehouseOperations) {

        getByIdHandlerMock.Setup(x => x.Handle(It.Is<GetWarehouseByIdQuery>(q => q.Id == warehouseId)))
                          .ReturnsAsync(warehouse);

        var result = await warehouseOperations.GetByIdAsync(warehouseId);

        result.Should().BeEquivalentTo(warehouse);
        getByIdHandlerMock.Verify(x => x.Handle(It.Is<GetWarehouseByIdQuery>(q => q.Id == warehouseId)), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task GetAllAsync_Should_Call_GetAllHandler_And_Return_Warehouses(
        IEnumerable<MvcAspAzure.Domain.Entity.Warehouse> warehouses,
        [Frozen] Mock<GetAllWarehousesHandler> getAllHandlerMock,
        WarehouseOperations warehouseOperations) {

        getAllHandlerMock.Setup(x => x.Handle(It.IsAny<GetAllWarehousesQuery>()))
                         .ReturnsAsync(warehouses);

        var result = await warehouseOperations.GetAllAsync();

        result.Should().BeEquivalentTo(warehouses);
        getAllHandlerMock.Verify(x => x.Handle(It.IsAny<GetAllWarehousesQuery>()), Times.Once);
    }
}
