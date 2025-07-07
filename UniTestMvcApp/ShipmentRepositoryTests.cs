using AutoFixture;
using AutoFixture.Xunit2;

using Microsoft.EntityFrameworkCore;

using Moq;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;


public class ShipmentRepositoryTests {

    private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class {
        var queryable = elements.AsQueryable();

        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        dbSetMock.Setup(d => d.ToListAsync(default)).ReturnsAsync(elements.ToList());

        return dbSetMock;
    }

    [Theory, AutoData]
    public async Task GetShipmentsByRouteIdAsync_ReturnsShipmentsWithMatchingRouteId(
        [Frozen] Fixture fixture,
        List<Shipment> shipments,
        int routeId) {

        foreach (var shipment in shipments.Take(3)) {
            shipment.RouteId = routeId;
        }
        foreach (var shipment in shipments.Skip(3)) {
            shipment.RouteId = routeId + 1;
        }

        var dbSetMock = CreateDbSetMock(shipments);
        var contextMock = new Mock<ShipmenDbContext>();
        contextMock.Setup(c => c.Shipment).Returns(dbSetMock.Object);

        var repo = new ShipmentRepository(contextMock.Object);

        var result = await repo.GetShipmentsByRouteIdAsync(routeId);

        Assert.That(result, Has.All.Matches<Shipment>(s => s.RouteId == routeId));
        Assert.Equals(3, result.Count());
    }

    [Theory, AutoData]
    public async Task GetShipmentsByCargoIdAsync_ReturnsShipmentsWithMatchingCargoId(
        [Frozen] Fixture fixture,
        List<Shipment> shipments,
        int cargoId) {

        foreach (var shipment in shipments.Take(2)) {
            shipment.CargoId = cargoId;
        }
        foreach (var shipment in shipments.Skip(2)) {
            shipment.CargoId = cargoId + 1; 
        }

        var dbSetMock = CreateDbSetMock(shipments);
        var contextMock = new Mock<ShipmenDbContext>();
        contextMock.Setup(c => c.Shipment).Returns(dbSetMock.Object);

        var repo = new ShipmentRepository(contextMock.Object);

        var result = await repo.GetShipmentsByCargoIdAsync(cargoId);

        Assert.That(result, Has.All.Matches<Shipment>(s => s.CargoId == cargoId));
        Assert.Equals(2, result.Count());
    }
}
