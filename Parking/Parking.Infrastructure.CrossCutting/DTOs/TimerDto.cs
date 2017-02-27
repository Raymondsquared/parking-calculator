using System;

namespace Parking.Infrastructure.CrossCutting.DTOs
{
    public class TimerDto
    {
        public DateTime Entry { get; set; }
        public DateTime Exit { get; set; }
    }
}
