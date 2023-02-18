namespace EmsisoftTest.Infrastructure.Interfaces;

public interface IQuery<in TContext, TResult> where TContext : IQueryContext
{
    Task<TResult> QueryAsync(TContext context);
}