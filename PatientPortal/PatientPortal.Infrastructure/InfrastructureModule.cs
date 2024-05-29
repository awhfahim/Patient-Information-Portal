using Autofac;
using PatientPortal.Application;
using PatientPortal.Domain.Repositories;
using PatientPortal.Infrastructure.Repositories;
using PatientPortal.Infrastructure.UnitOfWorks;

namespace PatientPortal.Infrastructure;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AllergieRepository>().As<IAllergieRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<DiseaseRepository>().As<IDiseaseInfoRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<NcdRepository>().As<INcdRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<PatientRepository>().As<IPatientRepository>()
            .InstancePerLifetimeScope();
    }
}