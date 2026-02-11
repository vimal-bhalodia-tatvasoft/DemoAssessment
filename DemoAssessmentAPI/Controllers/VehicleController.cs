using DemoAssessment.Domain;
using DemoAssessment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace DemoAssessmentAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleStatusService _vehicleStatusService;
        private readonly IModelService _modelService;
        public VehicleController()
        {
            _vehicleService = new VehicleService();
            _vehicleStatusService = new VehicleStatusService();
            _modelService = new ModelService();
        }

        [HttpGet("GetVehicles")]
        public IEnumerable<Vehicle> Get()
        {
			var abc="";
			var abc233="";
            var list = _vehicleService.GetVehicles();
            foreach (var item in list)
            {
                item.ModelName = _modelService.GetModels(item.ModelId)[0].ModelName;
                item.StatusName = _vehicleStatusService.GetVehicleStatuses().Where(x => x.StatusId == item.StatusId).FirstOrDefault().StatusName;
            }
            return list;
        }


        [HttpGet("GetVehiclesById")]
        public Vehicle GetVehiclesById(int VehicleId)
        {
            var model = new Vehicle();
            try
            {
                var list = _vehicleService.GetVehicles(VehicleId);
                if (list != null && list.Count > 0)
                {
                    model = list[0];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return model;
        }

        [HttpGet("AddUpdateVehicle")]
        public IActionResult AddUpdateVehicle(int VehicleId, int ModelId, int StatusId, string Year, string Color)
        {
            var model = new Vehicle();
            bool result = false;

            try
            {
                if (VehicleId == 0)
                {
                    model = new Vehicle
                    {
                        ModelId = ModelId,
                        StatusId = StatusId,
                        Year = Year,
                        Color = Color,
                        CreatedDate = DateTime.Now

                    };
                    result = _vehicleService.AddVehicle(model);
                }
                else
                {
                    var list = _vehicleService.GetVehicles(VehicleId);
                    if (list != null && list.Count > 0)
                    {
                        model = list[0];

                        model.ModelId =ModelId;
                        model.StatusId = StatusId;
                        model.Color = Color;
                        model.Year = Year;
                        model.ModifiedDate = DateTime.Now;
                        result = _vehicleService.UpdateVehicle(model);
                    }

                }
                return new ContentResult { StatusCode = 200, Content = result.ToString(), ContentType = "application/json" };

            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    return new ContentResult { Content = e.InnerException.Message, StatusCode = 404, ContentType = "application/json" };
                else
                    return new ContentResult { Content = e.Message, StatusCode = 404, ContentType = "application/json" };
            }

        }

        [HttpGet("DeleteVehicle")]
        public IActionResult DeleteVehicle(int VehicleId)
        {
            var model = new Vehicle();
            bool result = false;

            try
            {

                var list = _vehicleService.GetVehicles(VehicleId);
                if (list != null && list.Count > 0)
                {
                    model = list[0];
                    result = _vehicleService.DeleteVehicle(model);
                }

                return new ContentResult { StatusCode = 200, Content = result.ToString(), ContentType = "application/json" };

            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    return new ContentResult { Content = e.InnerException.Message, StatusCode = 404, ContentType = "application/json" };
                else
                    return new ContentResult { Content = e.Message, StatusCode = 404, ContentType = "application/json" };
            }

        }

        [HttpGet("GetStatuses")]
        public IEnumerable<VehicleStatus> GetStatuses()
        {
            var list = _vehicleStatusService.GetVehicleStatuses();
            return list;

        }
        
    }
}
