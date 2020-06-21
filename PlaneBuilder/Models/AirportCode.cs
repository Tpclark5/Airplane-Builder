using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{
    public class AirportCode
    {
        [BindProperty]
        public bool ArriveBy { get; set; }
        public string Location { get; set; }
        public string AirportName { get; set; }
        public string Code { get; set; }
        public DateTime Time { get; set; }

        public IEnumerable<Airport> data { get; set; }

        public class Airport
        {
            public string gmt { get; set; }
            public string iata_code { get; set; }
            public string city_iata_code { get; set; }
            public string icao_code { get; set; }
            public string country_iso2 { get; set; }
            public string geoname_id { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string airport_name { get; set; }
            public string country_name { get; set; }
            public string phone_number { get; set; }
            public string timezone { get; set; }
        }

    }
}
