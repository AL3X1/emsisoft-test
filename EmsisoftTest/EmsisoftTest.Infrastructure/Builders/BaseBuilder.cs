using Autofac;

namespace EmsisoftTest.Infrastructure.Builders;

public abstract class BaseBuilder
{
    protected readonly IComponentContext ComponentContext;

    protected BaseBuilder(IComponentContext componentContext)
    {
        ComponentContext = componentContext;
    }
}