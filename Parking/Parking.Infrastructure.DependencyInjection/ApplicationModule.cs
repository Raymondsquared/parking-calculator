using Autofac;
using Autofac.Core;
using Parking.Application.Abstractions;
using Parking.Application.Implementations;
using Parking.Infrastructure.CrossCutting;
using Parking.Infrastructure.CrossCutting.Abstractions;

namespace Parking.Infrastructure.DependencyInjection
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* Application Services */
            builder.RegisterType<ApplicationService>()
                .As<IApplicationService>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(IValidator<string>),
                        (pi, ctx) => ctx.ResolveKeyed<IValidator<string>>(Constants.ApplicationTypes.Console)))
                .SingleInstance();
        }
    }
}
