using PlaneBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public interface IAirplaneRepository
    {
        Task<bool> AddAirplane(AirplaneDBO dboAirplane);
        Task<IEnumerable<AirplaneDBO>> DisplayAllPlanes();
        Task<bool> UpdateSelectedPlane(AirplaneDBO dboAirplane);
        Task<AirplaneDBO> SelectOnePlane(int planeID);
        Task<bool> DeleteSelectedPlane(int planeID);
    }
}
