using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Models
{
    public class LoginModel
    {
        /// <summary>
        /// User Name
        /// </summary>
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        /// <summary>
        /// Remember Me
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
