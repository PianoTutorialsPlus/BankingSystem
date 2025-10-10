using BankingSystem.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Persistence.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly BankingDbContext _ctx;
    protected readonly DbSet<T> _db;

    public GenericRepository(BankingDbContext ctx)
    {
        _ctx = ctx;
        _db = ctx.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken ct = default) => await _db.AddAsync(entity, ct);
    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default) => await _db.FindAsync(new object[] { id }, ct) as T;
    public void Remove(T entity) => _db.Remove(entity);
    public void Update(T entity) => _db.Update(entity);
    public async Task<List<T>> ListAsync(CancellationToken ct = default) => await _db.AsNoTracking().ToListAsync(ct);
}
