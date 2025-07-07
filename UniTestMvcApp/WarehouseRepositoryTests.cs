using System.Linq.Expressions;

using AutoFixture.Xunit2;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using Moq;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;


public class WarehouseRepositoryTests {
    [Theory, AutoData]
    public async Task GetWarehouseByPlaceIdAsync_ReturnsMatchingWarehouses(List<Warehouse> warehouses, int placeId) {

        if (warehouses == null) warehouses = new List<Warehouse>();
        foreach (var w in warehouses)
            w.PlaceId = w.PlaceId == placeId ? placeId : placeId + 1;

        var mockSet = CreateMockDbSet(warehouses);

        var mockContext = new Mock<ShipmenDbContext>();
        mockContext.Setup(c => c.Warehouse).Returns(mockSet.Object);

        var repository = new WarehouseRepository(mockContext.Object);

        var result = await repository.GetWarehouseByPlaceIdAsync(placeId);

        Assert.IsTrue(result.All(w => w.PlaceId == placeId));
    }

    [Theory, AutoData]
    public async Task GetWarehouseByWarehouseIdAsync_ReturnsMatchingWarehouse(List<Warehouse> warehouses, int warehouseId) {

        if (warehouses == null) warehouses = new List<Warehouse>();
        foreach (var w in warehouses)
            w.Id = w.Id == warehouseId ? warehouseId : warehouseId + 1;

        var mockSet = CreateMockDbSet(warehouses);

        var mockContext = new Mock<ShipmenDbContext>();
        mockContext.Setup(c => c.Warehouse).Returns(mockSet.Object);

        var repository = new WarehouseRepository(mockContext.Object);

        var result = await repository.GetWarehouseByWarehouseIdAsync(warehouseId);

        Assert.IsTrue(result.All(w => w.Id == warehouseId));
    }

    private static Mock<DbSet<Warehouse>> CreateMockDbSet(List<Warehouse> data) {
        var queryable = data.AsQueryable();

        var mockSet = new Mock<DbSet<Warehouse>>();
        mockSet.As<IQueryable<Warehouse>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Warehouse>(queryable.Provider));
        mockSet.As<IQueryable<Warehouse>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<Warehouse>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<Warehouse>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.As<IAsyncEnumerable<Warehouse>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new AsyncEnumerator<Warehouse>(queryable.GetEnumerator()));

        return mockSet;
    }

    private class AsyncEnumerator<T> : IAsyncEnumerator<T> {
        private readonly IEnumerator<T> _inner;

        public AsyncEnumerator(IEnumerator<T> inner) => _inner = inner;

        public ValueTask DisposeAsync() {
            _inner.Dispose();
            return new ValueTask();
        }

        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());

        public T Current => _inner.Current;
    }

    private class AsyncQueryProvider<TEntity> : IAsyncQueryProvider {
        private readonly IQueryProvider _inner;

        public AsyncQueryProvider(IQueryProvider inner) => _inner = inner;

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
            => new AsyncEnumerable<TEntity>(expression);

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
            => new AsyncEnumerable<TElement>(expression);

        public object Execute(System.Linq.Expressions.Expression expression)
            => _inner.Execute(expression);

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
            => _inner.Execute<TResult>(expression);

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(System.Linq.Expressions.Expression expression)
            => new AsyncEnumerable<TResult>(expression);

        public Task<TResult> ExecuteAsync<TResult>(System.Linq.Expressions.Expression expression, CancellationToken cancellationToken)
            => Task.FromResult(Execute<TResult>(expression));

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

    private class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T> {
        public AsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable) { }

        public AsyncEnumerable(System.Linq.Expressions.Expression expression)
            : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

        IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
    }
}
