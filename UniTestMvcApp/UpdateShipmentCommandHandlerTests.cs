using AutoFixture.Xunit2;
using Moq;
using FluentAssertions;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Application.Shipment.Commands.DeleteShipment;
using MvcAspAzure.Application.Shipment.Commands.UpdateShipment;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;
using Xunit;

public class UpdateShipmentCommandHandlerTests {
    [Theory, AutoMoqData]
    public async Task Handle_WhenShipmentExists_UpdatesShipment(
        UpdateShipmentCommand command,
        Shipment existingShipment,
        [Frozen] Mock<IShipmentRepository> shipmentRepositoryMock,
        UpdateShipmentCommandHandler sut) {
        shipmentRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(existingShipment);

        await sut.Handle(command);

        existingShipment.CompletionData.Should().Be(command.CompletionData);
        existingShipment.RouteId.Should().Be(command.RouteId);
        existingShipment.Cargo.Should().Be(command.Cargo);

        shipmentRepositoryMock.Verify(r => r.UpdateAsync(existingShipment), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Handle_WhenShipmentDoesNotExist_DoesNotUpdate(
        UpdateShipmentCommand command,
        [Frozen] Mock<IShipmentRepository> shipmentRepositoryMock,
        UpdateShipmentCommandHandler sut) {
        shipmentRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync((Shipment?)null);

        await sut.Handle(command);

        shipmentRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Shipment>()), Times.Never);
    }
}

public class DeleteShipmentCommandHandlerTests {
    [Theory, AutoMoqData]
    public async Task Handle_WhenShipmentExists_DeletesShipment(
        DeleteShipmentCommand command,
        Shipment existingShipment,
        [Frozen] Mock<IShipmentRepository> shipmentRepositoryMock,
        DeleteShipmentCommandHandler sut) {
        shipmentRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync(existingShipment);

        await sut.Handle(command);

        shipmentRepositoryMock.Verify(r => r.DeleteAsync(existingShipment.Id), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task Handle_WhenShipmentDoesNotExist_DoesNotDelete(
        DeleteShipmentCommand command,
        [Frozen] Mock<IShipmentRepository> shipmentRepositoryMock,
        DeleteShipmentCommandHandler sut) {
        shipmentRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
            .ReturnsAsync((Shipment?)null);

        await sut.Handle(command);

        shipmentRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}

public class CreateShipmentCommandHandlerTests {
    [Theory, AutoMoqData]
    public async Task Handle_InsertsShipmentAndReturnsId(
        CreateShipmentCommand command,
        Shipment insertedShipment,
        [Frozen] Mock<IShipmentRepository> shipmentRepositoryMock,
        CreateShipmentCommandHandler sut) {
        insertedShipment.Id = command.Id;

        shipmentRepositoryMock.Setup(r => r.InsertAsync(It.IsAny<Shipment>()))
            .ReturnsAsync(insertedShipment);

        var result = await sut.Handle(command);

        shipmentRepositoryMock.Verify(r => r.InsertAsync(It.Is<Shipment>(s =>
            s.Id == command.Id &&
            s.StartData == command.StartData &&
            s.CompletionData == command.CompletionData &&
            s.RouteId == command.RouteId &&
            s.Route == command.Route &&
            s.Cargo == command.Cargo &&
            s.CargoId == command.CargoId
        )), Times.Once);

        result.Should().Be(insertedShipment.Id);
    }
}
