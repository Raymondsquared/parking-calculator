using System;
using System.Collections.Generic;
using System.Linq;
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

            var appService = container.Resolve<IApplicationService>();

            var dts = new List<DateTime>();

            var isValid = false;
            while (!isValid)
            {
                string msg;
                var input = new List<String>();

                Console.WriteLine("Enter entry date & time (DD/MM/YYYY HH:mm) : ");
                input.Add(Console.ReadLine());

                Console.WriteLine("Enter exit date & time  (DD/MM/YYYY HH:mm) : ");
                input.Add(Console.ReadLine());

                isValid = appService.ValidateInput(input, out msg, out dts);

                if (!isValid)
                {
                    Console.WriteLine(msg);
                }
            }

            try
            {
                var response = appService.ProcessAsync(dts).Result;

                Console.WriteLine("\n TOTAL COST : ");
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Error please try again.");
                Console.WriteLine(ex.Message + "\n");
            }            
        }        
    }
}
