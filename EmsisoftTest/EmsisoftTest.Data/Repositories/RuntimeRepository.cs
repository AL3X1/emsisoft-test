using EmsisoftTest.Data.Contexts;
using EmsisoftTest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmsisoftTest.Data.Repositories;

public class RuntimeRepository : IRuntimeRepository
{
    private readonly ApplicationDbContext _context;

    public RuntimeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class => GetDbSet<TEntity>().AsQueryable();
        
    public async Task<TEntity> AddAsync<TEntity>(TEntity entity, bool withSaving = true,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var result = await GetDbSet<TEntity>().AddAsync(entity, cancellationToken);

        if (withSaving)
        {
            await SaveAsync<TEntity>(cancellationToken);
        }

        return result.Entity;
    }

    public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool withSaving = true, CancellationToken cancellationToken = default) where TEntity : class
    {
        var result = GetDbSet<TEntity>().Update(entity);

        if (withSaving)
        {
            await SaveAsync<TEntity>(cancellationToken);
        }

        return result.Entity;
    }

    public async Task<List<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var updatedEntities = new List<TEntity>();
        foreach (var entity in entities)
        {
            var updatedEntity = await UpdateAsync(entity, withSaving, cancellationToken: cancellationToken);
            updatedEntities.Add(updatedEntity);
        }

        return updatedEntities;
    }

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        await GetDbSet<TEntity>().AddRangeAsync(entities, cancellationToken);

        if (withSaving)
        {
            await SaveAsync<TEntity>(cancellationToken);
        }
    }

    public async Task DeleteAsync<TEntity>(TEntity entity, bool withSaving = true, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        GetDbSet<TEntity>().Remove(entity);
        if (withSaving)
        {
            await SaveAsync<TEntity>(cancellationToken);
        }
    }

    public async Task DeleteRangeAsync<TEntity>(List<TEntity> entities, bool withSaving = true, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        GetDbSet<TEntity>().RemoveRange(entities);
        if (withSaving)
        {
            await SaveAsync<TEntity>(cancellationToken);
        }
    }

    public async Task<int> SaveAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class => _context.Set<TEntity>();
}