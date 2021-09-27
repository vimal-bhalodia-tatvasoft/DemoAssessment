using DemoAssessment.Services;
using DemoAssessmentAPI.Models;
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
    public class ManufacturerController : ControllerBase
    {

        private readonly IManufacturerService _manufacurerService;
        private readonly IModelService _modelService;
        public ManufacturerController(ILogger<ManufacturerController> logger)
        {
            _manufacurerService = new ManufacturerService();
            _modelService = new ModelService();
        }
        [HttpGet("GetManufacturers")]
        public IEnumerable<Manufacturer> Get()
        {
            var list = _manufacurerService.GetManufacturers();
            return list;
        }

        [HttpGet("GetManufacturersById")]
        public Manufacturer GetManufacturersById(int ManufacturerId)
        {
            var model = new Manufacturer();
            try
            {
                var list = _manufacurerService.GetManufacturers(ManufacturerId);
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

        [HttpGet("AddUpdateManufacturer")]
        public IActionResult AddUpdateManufacturer(int ManufacturerId, string ManufacturerName, string Country)
        {
            var model = new Manufacturer();
            bool result = false;

            try
            {
                if (ManufacturerId == 0)
                {
                    model = new Manufacturer
                    {
                        ManufacturerName = ManufacturerName,
                        Country = Country,
                        CreatedDate = DateTime.Now

                    };
                    result = _manufacurerService.AddManufacturer(model);
                }
                else
                {
                    var list = _manufacurerService.GetManufacturers(ManufacturerId);
                    if (list != null && list.Count > 0)
                    {
                        model = list[0];

                        model.ManufacturerName = ManufacturerName;
                        model.Country = Country;
                        model.ModifiedDate = DateTime.Now;
                        result = _manufacurerService.UpdateManufacturer(model);
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

        [HttpGet("DeleteManufacturer")]
        public IActionResult DeleteManufacturer(int ManufacturerId)
        {
            var model = new Manufacturer();
            bool result = false;

            try
            {
                var Modellist = _modelService.GetModelsbyManufacturerId(ManufacturerId);
                if (Modellist != null && Modellist.Count > 0)
                {
                    return new ContentResult { StatusCode = 200, Content = "Can't delete Manufacturer as it has referenced by Models.", ContentType = "application/json" };
                }

                var list = _manufacurerService.GetManufacturers(ManufacturerId);
                if (list != null && list.Count > 0)
                {
                    model = list[0];
                    result = _manufacurerService.DeleteManufacturer(model);
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
