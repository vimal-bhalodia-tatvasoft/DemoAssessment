using DemoAssessmentWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //string apiUrl = "https://localhost:44303/manufacturer/GetManufacturers";
           
            //WebClient client = new WebClient();
            //client.Headers["Content-type"] = "application/json";
            //client.Encoding = Encoding.UTF8;
            //string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("vimal.bhalodia@tatvasoft.com" +":" + "Demo@123"));
            //client.Headers[HttpRequestHeader.Authorization] = "Basic " +credentials;
            //var result = client.DownloadString(apiUrl);
            //DataTable dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));


            return View();
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
