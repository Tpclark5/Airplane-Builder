using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{
    public class AirplaneDBO
    {
        public int PlaneID { get; set; }
        public string Iata_Code { get; set; }
        public string Name { get; set; }
        public int Engine_Count { get; set; }
        public string Engine_Type { get; set; }
        public double Age { get; set; }
        public string Description { get; set; }
        public bool Does_Exist { get; set; }
        public bool Have_Ridden { get; set; }
        public string Rating { get; set; }
        public string Email_Address { get; set; }
        public string Plane_Status { get; set; }
        public string Picture { get; set; }
    }
}
