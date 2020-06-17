using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{
    public class Airplanes
    {

       
            public Pagination pagination { get; set; }
            public IEnumerable<AirplaneProperties> data { get; set; }
            public string engineType { get; set; }
        

        public class Pagination
        {
            public int offset { get; set; }
            public int limit { get; set; }
            public int count { get; set; }
            public int total { get; set; }
        }

        public class AirplaneProperties
        {
            public string iata_type { get; set; }
            public string airline_iata_code { get; set; }
            public string iata_code_short { get; set; }
            public object airline_icao_code { get; set; }
            public string construction_number { get; set; }
            public DateTime? delivery_date { get; set; }
            public string engines_count { get; set; }
            public string engines_type { get; set; }
            public object first_flight_date { get; set; }
            public string icao_code_hex { get; set; }
            public string line_number { get; set; }
            public string model_code { get; set; }
            public string registration_number { get; set; }
            public string test_registration_number { get; set; }
            public string plane_age { get; set; }
            public object plane_class { get; set; }
            public string model_name { get; set; }
            public string plane_owner { get; set; }
            public string plane_series { get; set; }
            public string plane_status { get; set; }
            public string production_line { get; set; }
            public object registration_date { get; set; }
            public object rollout_date { get; set; }
        }

    }
}
