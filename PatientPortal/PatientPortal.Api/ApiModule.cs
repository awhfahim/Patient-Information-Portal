using Autofac;
using PatientPortal.Api.RequestHandlers;

namespace PatientPortal.Api;

public class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GetPatientRequestHandler>()
            .AsSelf().InstancePerLifetimeScope();
    }
}