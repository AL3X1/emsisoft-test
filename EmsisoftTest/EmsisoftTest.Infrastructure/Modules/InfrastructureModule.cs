using Autofac;
using Module = Autofac.Module;

namespace EmsisoftTest.Infrastructure.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var projectAssemblyName = typeof(InfrastructureModule).FullName.Split(".").First();
            
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains(projectAssemblyName))
                .ToArray();
            
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
        }
    }
}