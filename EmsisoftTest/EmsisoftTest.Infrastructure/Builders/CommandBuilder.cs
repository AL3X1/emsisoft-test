using Autofac;
using EmsisoftTest.Infrastructure.Interfaces;

namespace EmsisoftTest.Infrastructure.Builders;

public class CommandBuilder : BaseBuilder, ICommandBuilder
{
    public CommandBuilder(IComponentContext componentContext) : base(componentContext)
    {
    }

    public async Task ExecuteAsync<TContext>(TContext context) where TContext : ICommandContext
    {
        var service = ComponentContext.Resolve<ICommand<TContext>>();
        await service.ExecuteAsync(context);
    }
}