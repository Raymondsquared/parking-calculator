using System;
using System.Collections.Generic;
using Autofac;
using Parking.Application.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;
using Parking.Infrastructure.CrossCutting.Validators;
using Parking.Infrastructure.DependencyInjection;

namespace Parking.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            /* Autofac */
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            IContainer container = builder.Build();
            
            var appService = container.Resolve<IApplicationService>();

            var timer = new TimerDto();
            var valid = new Validation();

            while (!valid.IsValid)
            {
                var input = new List<string>();

                System.Console.WriteLine("Enter entry date & time (DD/MM/YYYY HH:mm) : ");
                input.Add(System.Console.ReadLine());

                System.Console.WriteLine("Enter exit date & time  (DD/MM/YYYY HH:mm) : ");
                input.Add(System.Console.ReadLine());

                valid = appService.ValidateConsoleInput(input, out timer);
                if (!valid.IsValid)
                {
                    System.Console.WriteLine(valid.ErrorMessage);
                }
            }

            try
            {
                var response = appService.ProcessAsync(timer).Result;
                System.Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message + "\n");
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadLine();
        }        
    }
}
