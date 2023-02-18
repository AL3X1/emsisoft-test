using Autofac;
using EmsisoftTest.Infrastructure.Modules;

namespace EmsisoftTest.Infrastructure.Initializers;

public static class ContainerInitializer
{
    public static void Initialize(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule<InfrastructureModule>();
    }
}