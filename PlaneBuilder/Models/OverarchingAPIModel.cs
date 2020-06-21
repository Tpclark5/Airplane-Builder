using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{

    public class OverarchingAPIModel
    {
        public Pagination pagination { get; set; }
        public IEnumerable<Datum> data { get; set; }
    }

    public class Pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class Datum
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
}

   
