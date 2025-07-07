using Moq;
using AutoFixture;
using AutoFixture.AutoMoq;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse;
using MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById;
using MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses;
using MvcAspAzure.Application.Services.Operations;

[TestFixture]
public class WarehouseOperationsTests {
    private IFixture _fixture;
    private Mock<UpdateWarehouseCommandHandler> _updateHandlerMock;
    private Mock<CreateWarehouseCommandHandler> _createHandlerMock;
    private Mock<DeleteWarehouseCommandHandler> _deleteHandlerMock;
    private Mock<GetWarehouseByIdHandler> _getByIdHandlerMock;
    private Mock<GetAllWarehousesHandler> _getAllHandlerMock;

    private WarehouseOperations _warehouseOperations;

    [SetUp]
    public void Setup() {
        _fixture = new Fixture()
            .Customize(new AutoMoqCustomization { ConfigureMembers = true });


        _updateHandlerMock = _fixture.Freeze<Mock<UpdateWarehouseCommandHandler>>();
        _createHandlerMock = _fixture.Freeze<Mock<CreateWarehouseCommandHandler>>();
        _deleteHandlerMock = _fixture.Freeze<Mock<DeleteWarehouseCommandHandler>>();
        _getByIdHandlerMock = _fixture.Freeze<Mock<GetWarehouseByIdHandler>>();
        _getAllHandlerMock = _fixture.Freeze<Mock<GetAllWarehousesHandler>>();

        _warehouseOperations = new WarehouseOperations(
            _updateHandlerMock.Object,
            _createHandlerMock.Object,
            _deleteHandlerMock.Object,
            _getByIdHandlerMock.Object,
            _getAllHandlerMock.Object);
    }

    [Test]
    public async Task CreateAsync_Should_Call_CreateHandler_And_Return_Id() {
        var command = _fixture.Create<CreateWarehouseCommand>();
        var expectedId = _fixture.Create<int>();

        _createHandlerMock.Setup(x => x.Handle(command)).ReturnsAsync(expectedId);

        var result = await _warehouseOperations.CreateAsync(command);

        Assert.Equals(expectedId, result);
        _createHandlerMock.Verify(x => x.Handle(command), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_Should_Call_UpdateHandler() {
        var command = _fixture.Create<UpdateWarehouseCommand>();

        _updateHandlerMock.Setup(x => x.Handle(command)).Returns(Task.CompletedTask);

        await _warehouseOperations.UpdateAsync(command);

        _updateHandlerMock.Verify(x => x.Handle(command), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_Should_Call_DeleteHandler_With_Correct_Id() {
        var warehouseId = _fixture.Create<int>();

        _deleteHandlerMock.Setup(x => x.Handle(It.Is<DeleteWarehouseCommand>(cmd => cmd.Id == warehouseId)))
                          .Returns(Task.CompletedTask);

        await _warehouseOperations.DeleteAsync(warehouseId);

        _deleteHandlerMock.Verify(x => x.Handle(It.Is<DeleteWarehouseCommand>(cmd => cmd.Id == warehouseId)), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_Should_Call_GetByIdHandler_And_Return_Warehouse() {
        var warehouseId = _fixture.Create<int>();
        var warehouse = _fixture.Create<MvcAspAzure.Domain.Entity.Warehouse>();

        _getByIdHandlerMock.Setup(x => x.Handle(It.Is<GetWarehouseByIdQuery>(q => q.Id == warehouseId)))
                           .ReturnsAsync(warehouse);

        var result = await _warehouseOperations.GetByIdAsync(warehouseId);

        Assert.Equals(warehouse, result);
        _getByIdHandlerMock.Verify(x => x.Handle(It.Is<GetWarehouseByIdQuery>(q => q.Id == warehouseId)), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_Should_Call_GetAllHandler_And_Return_Warehouses() {
        var warehouses = _fixture.CreateMany<MvcAspAzure.Domain.Entity.Warehouse>(3);

        _getAllHandlerMock.Setup(x => x.Handle(It.IsAny<GetAllWarehousesQuery>()))
                          .ReturnsAsync(warehouses);

        var result = await _warehouseOperations.GetAllAsync();

        Assert.That(result, Is.EquivalentTo(warehouses));
        _getAllHandlerMock.Verify(x => x.Handle(It.IsAny<GetAllWarehousesQuery>()), Times.Once);
    }
}
