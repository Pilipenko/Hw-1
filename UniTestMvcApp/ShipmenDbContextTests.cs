using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;
using Xunit;

public class ShipmenDbContextAutoDataAttribute : AutoDataAttribute {
    public ShipmenDbContextAutoDataAttribute() : base(() => {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        var options = new DbContextOptionsBuilder<ShipmenDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        fixture.Inject(options);
        fixture.Register(() => new ShipmenDbContext(options));

        return fixture;
    }) { }
}

public class ShipmenDbContextTests {
    [Theory, ShipmenDbContextAutoData]
    public async Task Can_Add_And_Retrieve_Shipment(
        [Frozen] DbContextOptions<ShipmenDbContext> options,
        Shipment shipment) {
        using var dbContext = new ShipmenDbContext(options);

        var route = new Route { Id = shipment.RouteId };
        dbContext.Add(route);
        await dbContext.SaveChangesAsync();

        dbContext.Shipment.Add(shipment);
        await dbContext.SaveChangesAsync();

        var fromDb = await dbContext.Shipment.FindAsync(shipment.Id);

        Assert.NotNull(fromDb);
        Assert.Equal(shipment.Id, fromDb.Id);
        Assert.Equal(shipment.RouteId, fromDb.RouteId);
    }
}
