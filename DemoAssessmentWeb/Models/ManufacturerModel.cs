using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Models
{
    public class ManufacturerModel
    {
       
        public int ManufacturerId { get; set; }
        [Display(Name = "Manufacturer Name")]
        [Required(ErrorMessage = "Please enter manufacturer name")]
        [StringLength(200, ErrorMessage = "Please enter maximum 200 length of characters in name.")]
        public string ManufacturerName { get; set; }
        [Display(Name = "Country")]
        [StringLength(100, ErrorMessage = "Please enter maximum 200 length of characters in country.")]
        public string Country { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
