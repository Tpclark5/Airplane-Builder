using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlaneBuilder.Models;
using PlaneBuilder.Services;

namespace PlaneBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAirplaneClient _airplanes;

        private readonly IAirplaneRepository _airplaneRepository;

        public HomeController(IAirplaneClient airplanes, IAirplaneRepository airplaneRepository)
        {
            _airplanes = airplanes;
            _airplaneRepository = airplaneRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clientResult = await _airplanes.Airplanes();
            return View(clientResult);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Planes()
        {
            var model = new PlanesViewModel();
            var PlaneDBOList = await _airplaneRepository.DisplayAllPlanes();

            model.Planes = PlaneDBOList
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
            dboPlanes.Email_Address = postModel.EmailAddress;
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
