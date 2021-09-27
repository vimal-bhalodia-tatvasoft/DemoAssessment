using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Helpers
{
    public static class SessionHelper
    {
        public static IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        /// <summary>
        /// Get set UserName of the current login user.
        /// </summary>
        public static string UserName
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("UserName") != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
                }
                return "";
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserName", value);
            }
        }

        public static string Email
        {

            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("Email") != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString("Email").ToString();
                }
                return "";
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("Email", value);
            }

        }

        public static string Password
        {

            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("Password") != null)
                {
                    return _httpContextAccessor.HttpContext.Session.GetString("Password").ToString();
                }
                return "";
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("Password", value);
            }

        }


    }
}
