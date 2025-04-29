using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Repository;


namespace MvcAspAzure.Infrastructure.Repository {
    public sealed class RepositoryAsync<T> : IRepositoryAsync<T> where T : class {
        readonly ShipmenDbContext context;
        readonly DbSet<T> dbSet;

        public RepositoryAsync(ShipmenDbContext context) {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public async Task<T> InsertAsync(T entity) {
            //dbSet.Add(entity);
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity) {
            //context.Entry(entity).State = EntityState.Modified;
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var entity = await dbSet.FindAsync(id);
            if (entity != null) {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
