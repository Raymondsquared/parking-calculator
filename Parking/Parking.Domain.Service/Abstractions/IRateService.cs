using System;
using System.Collections.Generic;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Domain.Service.Abstractions
{
    public interface IRateService<T>
    {
        ParkingRateDto Calculate(IEnumerable<T> rates, DateTime start, DateTime end);
    }   
}
