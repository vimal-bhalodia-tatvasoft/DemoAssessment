using DemoAssessment.Domain;
using DemoAssessmentAPI.Helpers;
using DemoAssessmentWeb.Helpers;
using DemoAssessmentWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Grid()
        {

            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "vehicle/GetVehicles";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                var result = client.DownloadString(apiUrl);
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                return Json(result);
                // return View();
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [HttpGet]
        public IActionResult AddEditVehicle(int VehicleId = 0)
        {
            var editmodel = new VehicleModel();
            var ModelList = GetModelList();
            var StatusList = GetVehicleStatusList();
            editmodel.ModelList = ModelList;
            editmodel.StatusList = StatusList;
            try
            {
                if (VehicleId > 0)
                {
                    string apiUrl = AppSettings.GetAppSettings("AppUrl") + "vehicle/GetVehiclesById";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                    client.QueryString.Add("VehicleId", VehicleId.ToString());
                    var result = client.DownloadString(apiUrl);
                    Vehicle model = (Vehicle)JsonConvert.DeserializeObject(result, (typeof(Vehicle)));
                    if (model != null)
                    {
                        editmodel = new VehicleModel
                        {
                            VehicleId = model.VehicleId,
                            ModelId = model.ModelId,
                            StatusId = model.StatusId,
                            Year = model.Year,
                            Color = model.Color,
                            ModelName = model.ModelName,
                            StatusName = model.StatusName,
                            ModelList = ModelList,
                            StatusList = StatusList
                        };
                    }
                    return PartialView(editmodel);
                }
                else
                {
                    return PartialView(editmodel);

                }


            }
            catch (Exception e)
            {
                return View(editmodel);
            }
        }


        [HttpPost]
        public IActionResult AddEditVehicle(VehicleModel model)
        {
            var editmodel = new VehicleModel();
            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "vehicle/AddUpdateVehicle";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.QueryString.Add("ModelId", model.ModelId.ToString());
                client.QueryString.Add("VehicleId", model.VehicleId.ToString());
                client.QueryString.Add("StatusId", model.StatusId.ToString());
                client.QueryString.Add("Year", model.Year.ToString());
                client.QueryString.Add("Color", model.Color.ToString());
                var result = client.DownloadString(apiUrl);


            }
            catch (Exception e)
            {
                //return View(editmodel);
            }
            return RedirectToAction("Index", "Vehicle");
        }

        [HttpPost]
        public IActionResult DeleteVehicle(int VehicleId)
        {
            var editmodel = new ModelModel();
            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "Vehicle/DeleteVehicle";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.QueryString.Add("VehicleId", VehicleId.ToString());
                var result = client.DownloadString(apiUrl);

            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index", "Vehicle");
        }

        private List<SelectListItem> GetModelList()
        {
            var selectlist = new List<SelectListItem>();

            string apiUrl = AppSettings.GetAppSettings("AppUrl") + "model/GetModels";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            var result = client.DownloadString(apiUrl);
            List<Model> list = (List<Model>)JsonConvert.DeserializeObject(result, (typeof(List<Model>)));


            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    selectlist.Add(new SelectListItem { Text = item.ModelName, Value = item.ModelId.ToString() });

                }

            }
            return selectlist;
        }

        private List<SelectListItem> GetVehicleStatusList()
        {
            var selectlist = new List<SelectListItem>();

            string apiUrl = AppSettings.GetAppSettings("AppUrl") + "Vehicle/GetStatuses";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            var result = client.DownloadString(apiUrl);
            List<VehicleStatus> list = (List<VehicleStatus>)JsonConvert.DeserializeObject(result, (typeof(List<VehicleStatus>)));


            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    selectlist.Add(new SelectListItem { Text = item.StatusName, Value = item.StatusId.ToString() });

                }

            }
            return selectlist;
        }
    }
}
