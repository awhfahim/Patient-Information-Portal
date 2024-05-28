using Autofac;
using PatientPortal.Application.Contracts.Utilities;

namespace PatientPortal.Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<GuidProvider>().As<IGuidProvider>()
            .InstancePerLifetimeScope();
    }
}