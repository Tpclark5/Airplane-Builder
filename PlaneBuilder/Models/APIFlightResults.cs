using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{

    public class APIFlightResults
    {
        public string flight_date { get; set; }
        public string flight_status { get; set; }
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }
        public Airline airline { get; set; }
        public Flight flight { get; set; }
        public object aircraft { get; set; }
        public object live { get; set; }
    }

    public class Departure
    {
        public string airport { get; set; }
        public string timezone { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
        public object terminal { get; set; }
        public object gate { get; set; }
        public object delay { get; set; }
        public DateTime scheduled { get; set; }
        public DateTime estimated { get; set; }
        public object actual { get; set; }
        public object estimated_runway { get; set; }
        public object actual_runway { get; set; }
    }

    public class Arrival
    {
        public string airport { get; set; }
        public string timezone { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
        public object terminal { get; set; }
        public object gate { get; set; }
        public object baggage { get; set; }
        public object delay { get; set; }
        public DateTime scheduled { get; set; }
        public DateTime estimated { get; set; }
        public object actual { get; set; }
        public object estimated_runway { get; set; }
        public object actual_runway { get; set; }
    }

    public class Airline
    {
        public string name { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
    }

    public class Flight
    {
        public string number { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
        public object codeshared { get; set; }
    }

}
