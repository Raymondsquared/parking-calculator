using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.DependencyInjection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Parking.Test.Unit
{
    public class CalculatorCase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Expected { get; set; }
    }

    /*
     * 
     * 1/1/2017 is Sunday
     * 
     */

    [TestClass]
    public class CalculatorTest
    {
        private ICalculatorService _calculatorService;

        [TestInitialize]
        public void Init()
        {
            /* Autofac */
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainModule>();
            IContainer container = builder.Build();
            
            _calculatorService = container.Resolve<ICalculatorService>();
        }
        
        [TestMethod]
        public void GivenSpecialCondition_WhenEnterEarly_ThenReturnEarlyBirdRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                // Edge case
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 6, 0, 0),
                    End = new DateTime(2017, 1, 2, 15, 30, 0),
                    Expected = 13
                },
                // Edge case
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 9, 0, 0),
                    End = new DateTime(2017, 1, 2, 23, 30, 0),
                    Expected = 13
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 7, 0, 0),
                    End = new DateTime(2017, 1, 2, 10, 00, 0),
                    Expected = 13
                }
            };

            foreach (var c in cases)
            {
                var result = _calculatorService.Calculate(c.Start, c.End).Result;
                Assert.AreEqual(c.Expected, result);
            }
        }

        [TestMethod]
        public void GivenSpecialCondition_WhenEnterLate_ThenReturnNightRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                // Edge case
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 18, 0, 0),
                    End = new DateTime(2017, 1, 3, 6, 0, 0),
                    Expected = 6.5
                },
                // Edge case
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 3, 0, 0, 0),
                    End = new DateTime(2017, 1, 3, 6, 0, 0),
                    Expected = 6.5
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 20, 0, 0),
                    End = new DateTime(2017, 1, 3, 4, 0, 0),
                    Expected = 6.5
                }
            };

            foreach (var c in cases)
            {
                var result = _calculatorService.Calculate(c.Start, c.End).Result;
                Assert.AreEqual(c.Expected, result);
            }
        }

        [TestMethod]
        public void GivenSpecialCondition_WhenEnterWeekend_ThenReturnWeekendRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                // Edge case
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 7, 0, 0, 0),
                    End = new DateTime(2017, 1, 9, 0, 0, 0),
                    Expected = 10
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 7, 8, 0, 0),
                    End = new DateTime(2017, 1, 8, 20, 0, 0),
                    Expected = 10
                }
            };

            foreach (var c in cases)
            {
                var result = _calculatorService.Calculate(c.Start, c.End).Result;
                Assert.AreEqual(c.Expected, result);
            }
        }

        [TestMethod]
        public void GivenNormalCondition_WhenEnter_ThenReturnNormalRate()
        {
            List<CalculatorCase> cases = new List<CalculatorCase>()
            {
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 9, 0, 0),
                    End = new DateTime(2017, 1, 2, 10, 0, 0),
                    Expected = 5
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 10, 0, 0),
                    End = new DateTime(2017, 1, 2, 12, 0, 0),
                    Expected = 10
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 12, 0, 0),
                    End = new DateTime(2017, 1, 2, 15, 0, 0),
                    Expected = 15
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 15, 0, 0),
                    End = new DateTime(2017, 1, 2, 19, 0, 0),
                    Expected = 20
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 12, 0, 0),
                    End = new DateTime(2017, 1, 4, 12, 0, 0),
                    Expected = 40
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 12, 0, 0),
                    End = new DateTime(2017, 1, 4, 15, 0, 0),
                    Expected = 55
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 12, 0, 0),
                    End = new DateTime(2017, 1, 4, 19, 0, 0),
                    Expected = 60
                },
                new CalculatorCase()
                {
                    Start = new DateTime(2017, 1, 2, 12, 0, 0),
                    End = new DateTime(2017, 1, 2, 12, 15, 0),
                    Expected = 5
                }
            };

            foreach (var c in cases)
            {
                var result = _calculatorService.Calculate(c.Start, c.End).Result;
                Assert.AreEqual(c.Expected, result);
            }
        }
    }
}
