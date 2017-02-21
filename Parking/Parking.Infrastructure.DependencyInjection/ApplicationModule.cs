using Autofac;
using Parking.Application.Abstractions;
using Parking.Application.Implementations;

namespace Parking.Infrastructure.DependencyInjection
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* Application Services */
            builder.RegisterType<ApplicationService>()
                .As<IApplicationService>()
                .SingleInstance();
        }
    }
}
