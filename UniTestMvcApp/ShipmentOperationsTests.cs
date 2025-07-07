using AutoFixture.Xunit2;
using Moq;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Application.Services.Operations;
using MvcAspAzure.Application.Shipment.Commands.UpdateShipment;
using MvcAspAzure.Application.Shipment.Commands.DeleteShipment;
using MvcAspAzure.Application.Shipment.Queries.GetShipmentById;
using MvcAspAzure.Application.Shipment.Queries.GetAllShipments;

public class ShipmentOperationsTests {
    [Theory, AutoData]
    public async Task CreateAsync_ShouldCallCreateHandler(
        [Frozen] Mock<CreateShipmentCommandHandler> createHandlerMock,
        CreateShipmentCommand command,
        ShipmentOperations sut) {
        createHandlerMock.Setup(h => h.Handle(command)).ReturnsAsync(123);

        var result = await sut.CreateAsync(command);

        Assert.Equals(123, result);
        createHandlerMock.Verify(h => h.Handle(command), Times.Once);
    }

    [Theory, AutoData]
    public async Task UpdateAsync_ShouldCallUpdateHandler(
        [Frozen] Mock<UpdateShipmentCommandHandler> updateHandlerMock,
        UpdateShipmentCommand command,
        ShipmentOperations sut) {
        await sut.UpdateAsync(command);

        updateHandlerMock.Verify(h => h.Handle(command), Times.Once);
    }

    [Theory, AutoData]
    public async Task DeleteAsync_ShouldCallDeleteHandler(
        [Frozen] Mock<DeleteShipmentCommandHandler> deleteHandlerMock,
        int shipmentId,
        ShipmentOperations sut) {
        await sut.DeleteAsync(shipmentId);

        deleteHandlerMock.Verify(h =>
            h.Handle(It.Is<DeleteShipmentCommand>(cmd => cmd.Id == shipmentId)), Times.Once);
    }

    [Theory, AutoData]
    public async Task GetByIdAsync_ShouldCallGetByIdHandler(
        [Frozen] Mock<GetShipmentByIdHandler> getByIdHandlerMock,
        Shipment expectedShipment,
        int shipmentId,
        ShipmentOperations sut) {
        getByIdHandlerMock
            .Setup(h => h.Handle(It.Is<GetShipmentByIdQuery>(q => q.Id == shipmentId)))
            .ReturnsAsync(expectedShipment);

        var result = await sut.GetByIdAsync(shipmentId);

        Assert.Equals(expectedShipment, result);
    }

    [Theory, AutoData]
    public async Task GetAllAsync_ShouldCallGetAllHandler(
        [Frozen] Mock<GetAllShipmentsHandler> getAllHandlerMock,
        IEnumerable<Shipment> expectedShipments,
        ShipmentOperations sut) {
        getAllHandlerMock
            .Setup(h => h.Handle(It.IsAny<GetAllShipmentsQuery>()))
            .ReturnsAsync(expectedShipments);

        var result = await sut.GetAllAsync();

        Assert.Equals(expectedShipments, result);
    }
}