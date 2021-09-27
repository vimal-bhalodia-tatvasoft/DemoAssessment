using DemoAssessment.Domain;
using DemoAssessment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IVehicleService _vehicleService;
        public ModelController()
        {
            _modelService = new ModelService();
            _manufacturerService = new ManufacturerService();
            _vehicleService = new VehicleService();
        }
        [HttpGet("GetModels")]
        public IEnumerable<Model> Get()
        {
            var list = _modelService.GetModels();
            foreach (var item in list)
            {
                item.ManufacturerName = _manufacturerService.GetManufacturers(item.ManufacturerId)[0].ManufacturerName;
            }
            return list;
        }


        [HttpGet("GetModelsById")]
        public Model GetModelsById(int ModelId)
        {
            var model = new Model();
            try
            {
                var list = _modelService.GetModels(ModelId);
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

        [HttpGet("AddUpdateModel")]
        public IActionResult AddUpdateModel(int ModelId, int ManufacturerId, string ModelName,DateTime FirstProductionDate)
        {
            var model = new Model();
            bool result = false;

            try
            {
                if (ModelId == 0)
                {
                    model = new Model
                    {
                        ModelName = ModelName,
                        ManufacturerId = ManufacturerId,
                        FirstProductionDate = FirstProductionDate,
                        CreatedDate = DateTime.Now

                    };
                    result = _modelService.AddModel(model);
                }
                else
                {
                    var list = _modelService.GetModels(ModelId);
                    if (list != null && list.Count > 0)
                    {
                        model = list[0];

                        model.ModelName = ModelName;
                        model.FirstProductionDate = FirstProductionDate;
                        model.ManufacturerId = ManufacturerId;
                        model.ModifiedDate = DateTime.Now;
                        result = _modelService.UpdateModel(model);
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

        [HttpGet("DeleteModel")]
        public IActionResult DeleteModel(int ModelId)
        {
            var model = new Model();
            bool result = false;

            try
            {
                var vehicleList = _vehicleService.GetVehiclesbyModelId(ModelId);
                if (vehicleList != null && vehicleList.Count > 0)
                {
                    return new ContentResult { StatusCode = 200, Content = "Can't delete Model as it has referenced by Vehicles.", ContentType = "application/json" };
                }


                var list = _modelService.GetModels(ModelId);
                if (list != null && list.Count > 0)
                {
                    model = list[0];
                    result = _modelService.DeleteModel(model);
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
    }
}
