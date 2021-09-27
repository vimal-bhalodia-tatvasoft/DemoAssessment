using DemoAssessmentAPI.Helpers;
using DemoAssessmentAPI.Models;
using DemoAssessmentWeb.Helpers;
using DemoAssessmentWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ManufacturerController : Controller
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
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "manufacturer/GetManufacturers";

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
        public IActionResult AddEditManufacturer(int ManufacturerId=0)
        {
            var editmodel = new ManufacturerModel();
            try
            {
                if (ManufacturerId > 0)
                {
                    string apiUrl = AppSettings.GetAppSettings("AppUrl") +"manufacturer/GetManufacturersById";

                    WebClient client = new WebClient();
                    client.Headers["Content-type"] = "application/json";
                    client.Encoding = Encoding.UTF8;
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                    client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                    client.QueryString.Add("ManufacturerId", ManufacturerId.ToString());
                    var result = client.DownloadString(apiUrl);
                    Manufacturer model = (Manufacturer)JsonConvert.DeserializeObject(result, (typeof(Manufacturer)));
                    if (model != null)
                    {
                        editmodel = new ManufacturerModel
                        {
                            ManufacturerId = model.ManufacturerId,
                            ManufacturerName = model.ManufacturerName,
                            Country = model.Country
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
        public IActionResult AddEditManufacturer(ManufacturerModel model)
        {
            var editmodel = new ManufacturerModel();
            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "manufacturer/AddUpdateManufacturer";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.QueryString.Add("ManufacturerId", model.ManufacturerId.ToString());
                client.QueryString.Add("ManufacturerName", model.ManufacturerName.ToString());
                client.QueryString.Add("Country", model.Country.ToString());
                var result = client.DownloadString(apiUrl);


                

            }
            catch (Exception e)
            {
                //return View(editmodel);
            }
            return RedirectToAction("Index", "Manufacturer");
        }

        [HttpPost]
        public IActionResult DeleteManufacturer(int ManufacturerId)
        {
            var editmodel = new ManufacturerModel();
            try
            {
                string apiUrl = AppSettings.GetAppSettings("AppUrl") + "manufacturer/DeleteManufacturer";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(SessionHelper.Email + ":" + SessionHelper.Password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.QueryString.Add("ManufacturerId", ManufacturerId.ToString());
                var result = client.DownloadString(apiUrl);
                return Json(result);
            }
            catch (Exception e)
            {
                
            }
            return RedirectToAction("Index", "Manufacturer");
        }


    }
}
