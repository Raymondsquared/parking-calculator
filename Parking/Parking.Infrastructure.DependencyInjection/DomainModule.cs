using Autofac;
using Parking.Domain.Model;
using Parking.Domain.Service.Abstractions;
using Parking.Domain.Service.Implementations;
using Parking.Infrastructure.CrossCutting.Abstractions;

namespace Parking.Infrastructure.DependencyInjection
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* Domain Services */
            builder.RegisterType<CalculatorService>()                
                .As<ICalculatorService>()
                .SingleInstance();

            builder.RegisterType<SpecialService>()
                .As<IDomainService<Special>>()
                .As<ISpecialService>()
                .SingleInstance();

            builder.RegisterType<NormalService>()
                .As<IDomainService<Normal>>()
                .As<INormalService>()
                .SingleInstance();
        }
    }
}
