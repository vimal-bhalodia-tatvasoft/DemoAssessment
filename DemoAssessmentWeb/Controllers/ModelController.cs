using DemoAssessment.Domain;
using DemoAssessmentAPI.Helpers;
using DemoAssessmentAPI.Models;
using DemoAssessmentWeb.Helpers;
using DemoAssessmentWeb.Models;
using Identity.Models;
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
    public class ModelController : Controller
    {
        #region Fields

        private DemoDbContext _dbContext;

        #endregion

        #region Ctor

        public ModelController()
        {
            _dbContext = new DemoDbContext();
        }

        #endregion
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Grid()
        {

            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "model/GetModels";

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
        public IActionResult AddEditModel(int ModelId = 0)
        {
            var editmodel = new ModelModel();
            var ManufacturerSelectList = GetManufacturerList();
            editmodel.ManufacturerList = ManufacturerSelectList;
            try
            {
                if (ModelId > 0)
                {
                    string apiUrl = AppSettings.GetAppSettings("AppUrl") + "model/GetModelsById";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                    client.QueryString.Add("ModelId", ModelId.ToString());
                    var result = client.DownloadString(apiUrl);
                    Model model = (Model)JsonConvert.DeserializeObject(result, (typeof(Model)));
                    if (model != null)
                    {
                        editmodel = new ModelModel
                        {
                            ModelId = model.ModelId,
                            FirstProductionDate = model.FirstProductionDate,
                            ModelName = model.ModelName,
                            ManufacturerId = model.ManufacturerId,
                            ManufacturerList = ManufacturerSelectList
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
        public IActionResult AddEditModel(ModelModel model)
        {
            var editmodel = new ModelModel();
            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "model/AddUpdateModel";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.QueryString.Add("ModelId", model.ModelId.ToString());
                client.QueryString.Add("ManufacturerId", model.ManufacturerId.ToString());
                client.QueryString.Add("ModelName", model.ModelName.ToString());
                client.QueryString.Add("FirstProductionDate", model.FirstProductionDate.ToString("dd-MMM-yyyy"));
                var result = client.DownloadString(apiUrl);


            }
            catch (Exception e)
            {
                //return View(editmodel);
            }
            return RedirectToAction("Index", "Model");
        }

        [HttpPost]
        public IActionResult DeleteModel(int ModelId)
        {
            var editmodel = new ModelModel();
            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "model/DeleteModel";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.QueryString.Add("ModelId", ModelId.ToString());
                var result = client.DownloadString(apiUrl);
                return Json(result);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index", "Model");
        }

        private List<SelectListItem> GetManufacturerList()
        {
            var selectlist = new List<SelectListItem>();

            string apiUrl = AppSettings.GetAppSettings("AppUrl") + "manufacturer/GetManufacturers";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            var result = client.DownloadString(apiUrl);
            List<Manufacturer> list = (List<Manufacturer>)JsonConvert.DeserializeObject(result, (typeof(List<Manufacturer>)));


            if (list != null && list.Count>0)
            {
                foreach (var item in list)
                {
                    selectlist.Add(new SelectListItem { Text = item.ManufacturerName, Value = item.ManufacturerId.ToString() });

                }
            
            }
            return selectlist;
        }
    }
}
