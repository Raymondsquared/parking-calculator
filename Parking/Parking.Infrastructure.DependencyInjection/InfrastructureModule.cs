using Autofac;
using Parking.Domain.Model;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.Repositories;

namespace Parking.Infrastructure.DependencyInjection
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* Repositories */
            builder.RegisterType<SpecialJsonRepository>()
                .As<IRepository<Special>>()
                .As<ISpecialRepository>()
                .SingleInstance();

            builder.RegisterType<NormalJsonRepository>()
                .As<IRepository<Normal>>()
                .As<INormalRepository>()
                .SingleInstance();
        }
    }
}
