namespace EmsisoftTest.Infrastructure.Interfaces;

public interface IQueryBuilder
{
    Task<TResult> AskAsync<TContext, TResult>(TContext context) where TContext : IQueryContext;
}