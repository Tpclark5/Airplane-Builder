using PlaneBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public interface IAirplaneClient
    {
        Task<Airplanes> Airplanes();
    }
}