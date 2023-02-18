using Autofac;
using EmsisoftTest.Infrastructure.Interfaces;

namespace EmsisoftTest.Infrastructure.Builders;

public class QueryBuilder : BaseBuilder, IQueryBuilder
{
    public QueryBuilder(IComponentContext componentContext) : base(componentContext)
    {
    }

    public async Task<TResult> AskAsync<TContext, TResult>(TContext context) where TContext : IQueryContext
    {
        var service = ComponentContext.Resolve<IQuery<TContext, TResult>>();
        return await service.QueryAsync(context);
    }
}