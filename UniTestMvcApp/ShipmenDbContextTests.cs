using AutoFixture;
using AutoFixture.Xunit2;

using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;

public class ShipmenDbContextAutoDataAttribute : AutoDataAttribute {
    public ShipmenDbContextAutoDataAttribute() : base(static () => {
        var fixture = new Fixture();

        var options = new DbContextOptionsBuilder<ShipmenDbContext>()
            .UseInMemoryDatabase(databaseName: "Shipment")
            .Options;

        fixture.Inject(options);
        return fixture;
    }) { }
}

public class ShipmenDbContextTests {
    [Theory, ShipmenDbContextAutoData]
    public async Task Can_Add_And_Retrieve_Shipment(
        ShipmenDbContext dbContext,
        Shipment shipment) {

        var route = new Route { Id = shipment.RouteId };
        dbContext.Add(route);
        await dbContext.SaveChangesAsync();

        dbContext.Shipment.Add(shipment);
        await dbContext.SaveChangesAsync();

        var fromDb = await dbContext.Shipment.FindAsync(shipment.Id);
        Assert.NotNull(fromDb);
        Assert.Equals(shipment.Id, fromDb.Id);
        Assert.Equals(shipment.RouteId, fromDb.RouteId);
    }
}
