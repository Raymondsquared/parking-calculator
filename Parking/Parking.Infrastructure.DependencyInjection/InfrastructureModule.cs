using Autofac;
using Parking.Domain.Model;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting;
using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;
using Parking.Infrastructure.CrossCutting.Validators.Implementations;
using Parking.Infrastructure.Repositories;

namespace Parking.Infrastructure.DependencyInjection
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            
            /* Validators */
            builder.RegisterType<InputStringValidator>()
                .As<IValidator<string>>()
                .Keyed<IValidator<string>>(CONSTANTS.APPLICATION_TYPES.CONSOLE);
            builder.RegisterType<InputDateValidator>()
                .As<IValidator<string>>()
                .Keyed<IValidator<string>>(CONSTANTS.APPLICATION_TYPES.CONSOLE);
            
            builder.RegisterType<InputDatesValidator>()
                .As<IValidator<TimerDto>>();

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
