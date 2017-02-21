using System.Collections.Generic;
using Autofac;
using Parking.Application.Abstractions;
using Parking.Infrastructure.DependencyInjection;

namespace Parking
{
    class Program
    {
        public static void Main(string[] args)
        {
            /* Autofac */
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            IContainer container = builder.Build();

            List<string> input = new List<string>()
            {
                "01/01/2017",
                "07:00",
                "01/02/2017",
                "23:00"
            };

            var appService = container.Resolve<IApplicationService>();

            var response = appService.ProcessAsync(input).Result;

            System.Console.WriteLine(response);
        }
    }
}
