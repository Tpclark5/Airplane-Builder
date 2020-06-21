using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{
    public class APISimpleResponse
    {
        public IEnumerable<Flight> Flights { get; set; }
        public IEnumerable<Departure> Departures { get; set; }
    }
}
