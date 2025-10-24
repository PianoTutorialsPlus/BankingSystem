using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Domain.Common;
using BankingSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly BankingDbContext context;

    public GenericRepository(BankingDbContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        // Check if an entity with the same key is already being tracked
        var trackedEntity = context.Set<T>()
            .Local
            .FirstOrDefault(e => e.Id == entity.Id);

        if (trackedEntity != null)
        {
            // Detach the already tracked instance
            context.Entry(trackedEntity).State = EntityState.Detached;
        }

        // Attach the entity we want to delete (in case it's not tracked)
        context.Attach(entity);
        context.Remove(entity);

        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await context.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}
