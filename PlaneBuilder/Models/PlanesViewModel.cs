using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{
    public class PlanesViewModel
    {
        public IEnumerable<PlaneNameandPlaneID> Planes { get; set; }
    }
}
