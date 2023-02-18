namespace EmsisoftTest.Data.Interfaces;

public interface IRuntimeRepository
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;

    Task<TEntity> AddAsync<TEntity>(TEntity entity, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class;

    Task<List<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities,
        bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class;

    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class;

    Task DeleteAsync<TEntity>(TEntity entity, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class;
    
    Task DeleteRangeAsync<TEntity>(List<TEntity> entities, bool withSaving = true,
        CancellationToken cancellationToken = default) where TEntity : class;

    Task<int> SaveAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : class;
}