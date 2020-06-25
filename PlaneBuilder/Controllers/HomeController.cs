using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlaneBuilder.Models;
using PlaneBuilder.Services;

namespace PlaneBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAirplaneClient _airplanes;

        private readonly IAirplaneRepository _airplaneRepository;

        private readonly ITSAClient _airportClient;
        private readonly IAirportCodeClient _airportCodeClient;

        private readonly string _googleClient;

        public HomeController(IAirplaneClient airplanes, IAirplaneRepository airplaneRepository, ITSAClient airportClient, IAirportCodeClient airportCodeClient, IOptions<APISecretConfig>googleClient)
        {
            _airplanes = airplanes;
            _airplaneRepository = airplaneRepository;
            _airportClient = airportClient;
            _airportCodeClient = airportCodeClient;
            _googleClient = googleClient.Value.GoogleKey;
        }

        public async Task<IActionResult> Index()
        {
            var clientResult = await _airplanes.Airplanes();
            return View(clientResult);
        }

        public async Task<IActionResult> TSASpecs(Departure model)
        {
            AirportCode newModel = new AirportCode();
            newModel.Code = model.iata;
            return View(newModel);
        }

        public async Task<IActionResult> GetWaitTime(AirportCode model)

        {
            var clientResult = await _airportClient.GetAirport(model.Code);

            return View(clientResult);
        }

        public async Task<IActionResult> TravelTime(AirportCode model)
        {
            TravelTimeViewModel travel = new TravelTimeViewModel();
            travel.Location = model.Location;
            travel.ArriveBy = model.ArriveBy;
            string time = Request.Form["time"];
            model.Code = Request.Form["airport"];
            var tsaWaitTime = await _airportClient.GetAirport(model.Code);
            travel.Code = model.Code;
            travel.TSAWaitTime = tsaWaitTime.rightnow / 60;
            string hourMin = time.Substring(0, 5);
            time = DateTime.Parse(hourMin).ToString("h:mm tt");
            model.Time = Convert.ToDateTime(time);
            travel.Time = model.Time.ToString("h:mm tt");
            DirectionsRequest request = new DirectionsRequest();
            request.Key = "AIzaSyAD_-v70Gc1IQ2mfHkKTjCYBINKMlQ4I8I";
            request.Origin = new Location(model.Location);
            request.Destination = new Location(model.Code);
            var response = GoogleApi.GoogleMaps.Directions.Query(request);

            double duration = response.Routes.First().Legs.First().Duration.Value / 3600D;
            travel.TotalTravelTime = duration + travel.TSAWaitTime;

            TimeSpan tt = TimeSpan.FromHours((duration));
            travel.DriveTime = tt.Hours.ToString("00") + " hours" + " and" + tt.Minutes.ToString(" 00") + " minutes";

            if (model.ArriveBy)
            {
                DateTime arriveByTime = model.Time;
                DateTime updatedTime = arriveByTime.AddHours(-(travel.TotalTravelTime));
                travel.LeaveTime = updatedTime.ToString("h:mm tt");
            }
            else
            {
                DateTime arriveByTime = model.Time;
                DateTime updatedTime = arriveByTime.AddHours((travel.TotalTravelTime));
                travel.LeaveTime = updatedTime.ToString("h:mm tt");
            }

            ;
            return View(travel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Planes()
        {
            var model = new PlanesViewModel();
            var PlaneDBOList = await _airplaneRepository.DisplayAllPlanes();

            var specificUserPlanes = PlaneDBOList.Where(planeList => planeList.Email_Address == User.Identity.Name)
            .ToList();

            model.Planes = specificUserPlanes
                .Select(PlaneDBO => new PlaneNameandPlaneID() { name = PlaneDBO.Name, PlaneID = PlaneDBO.PlaneID })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Planes(PlanesViewModel model)
        {

            var PlaneDBOList = await _airplaneRepository.DisplayAllPlanes();

            model.Planes = PlaneDBOList
                .Select(PlaneDBO => new PlaneNameandPlaneID() { name = PlaneDBO.Name, PlaneID = PlaneDBO.PlaneID })
                .ToList();

            return RedirectToAction(nameof(Planes));
        }

        [HttpGet]
        public IActionResult AddPlane()
        {
            var model = new AddPlaneViewModel(); //pending review
            return View(model);
        }

        [HttpPost]
        public IActionResult AddPlane(AddPlaneViewModel postModel)
        {
            var dboPlanes = new AirplaneDBO();
            dboPlanes.Name = postModel.Name;
            dboPlanes.Have_Ridden = postModel.Have_Ridden;
            dboPlanes.Engine_Type = postModel.Engine_Type;
            dboPlanes.Age = postModel.Age;
            dboPlanes.Description = postModel.Description;
            dboPlanes.Does_Exist = postModel.Does_Exist;
            dboPlanes.Email_Address = User.Identity.Name;
            dboPlanes.Engine_Count = postModel.Engine_Count;
            dboPlanes.Plane_Status = postModel.Plane_Status;
            dboPlanes.Picture = postModel.Picture;
            dboPlanes.Rating = postModel.Rating;


            _airplaneRepository.AddAirplane(dboPlanes);

            return RedirectToAction(nameof(Planes));
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePlane(int planeId)
        {
            var model = new UpdatePlaneViewModel();

            var currentPlane = await _airplaneRepository.SelectOnePlane(planeId);

            model.NewAge = 0;
            model.OldAge = currentPlane.Age;

            model.NewDescription = string.Empty;
            model.OldDescription = currentPlane.Description;

            model.NewDoes_Exist = false;
            model.OldDoes_Exist = currentPlane.Does_Exist;

            model.NewEngine_Count = 0;
            model.OldEngine_Count = currentPlane.Engine_Count;

            model.NewEngine_Type = string.Empty;
            model.OldEngine_Type = currentPlane.Engine_Type;

            model.NewHave_Ridden = false;
            model.OldHave_Ridden = currentPlane.Have_Ridden;

            model.Newiatacode = string.Empty;
            model.Oldiatacode = currentPlane.Iata_Code;

            model.NewName = string.Empty;
            model.OldName = currentPlane.Name;

            model.NewPicture = string.Empty;
            model.OldPicture = currentPlane.Picture;

            model.NewRating = string.Empty;
            model.OldRating = currentPlane.Rating;

            model.OldPlane_Status = currentPlane.Plane_Status;

            model.OldEmailAddress = currentPlane.Email_Address;

            model.PlaneID = planeId;

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdatePlane(UpdatePlaneViewModel model)
        {
            var dboPlane = new AirplaneDBO();
            dboPlane.Age = model.NewAge;
            dboPlane.Description = model.NewDescription;
            dboPlane.Does_Exist = model.NewDoes_Exist;
            dboPlane.Email_Address = model.NewEmailAddress;
            dboPlane.Engine_Count = model.NewEngine_Count;
            dboPlane.Engine_Type = model.NewEngine_Type;
            dboPlane.Have_Ridden = model.NewHave_Ridden;
            dboPlane.Iata_Code = model.Newiatacode;
            dboPlane.Name = model.NewName;
            dboPlane.Picture = model.NewPicture;
            dboPlane.Plane_Status = model.NewPlane_Status;
            dboPlane.Rating = model.NewRating;
            dboPlane.PlaneID = model.PlaneID;

            _airplaneRepository.UpdateSelectedPlane(dboPlane);

            return RedirectToAction(nameof(Planes));
        }

        [HttpGet]
        public IActionResult DeleteSelectedPlane(int planeId)
        {
            _airplaneRepository.DeleteSelectedPlane(planeId);
            return RedirectToAction(nameof(Planes));
        }
        [HttpGet]
        public async Task<IActionResult> FindAPlane(int planeId)
        {
            var coolPlane = await _airplaneRepository.SelectOnePlane(planeId);
            var actualPlane = await _airplanes.FindAPlane(coolPlane);
            var PlaneList = actualPlane.data.ToList();
            var planeDeparture = await _airplanes.FindAnAirport(PlaneList);

            return View(planeDeparture);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
