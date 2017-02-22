using System;
using System.Collections.Generic;

namespace Parking.Domain.Model
{
    public class Duration
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public IEnumerable<string> Days { get; set;}
    }
}
