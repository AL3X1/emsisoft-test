using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace EmsisoftTest.Infrastructure.Modules;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var solutionAssemblyPrefix = typeof(InfrastructureModule).FullName.Split(".").First();
      
        var assemblies = new[]
        {
            "Data",
            "Messaging",
            "Infrastructure",
            "Domain",
        }
            .Select(x => Assembly.Load(string.Join(".", solutionAssemblyPrefix, x)))
            .ToArray();

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
    }
}