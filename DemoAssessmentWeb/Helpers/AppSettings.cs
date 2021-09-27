using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentAPI.Helpers
{

    public class AppSettings
    {
        /// <summary>
        /// Get AppSettings from appsettings file
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        internal static string GetAppSettings(string settingName = null, bool IsFromAppsetting = true)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            if (IsFromAppsetting == true)
            {
                return root.GetSection("ApplicationSettings").GetSection(settingName).Value;
            }
            else
            {
                return root.GetSection("ConnectionStrings").GetSection(settingName).Value;
            }
        }

    }
}
