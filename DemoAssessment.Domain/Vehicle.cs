using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Domain
{
    [Table("Vehicle", Schema = "dbo")]
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public int ModelId { get; set; }
        public int StatusId { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [NotMapped]
        public string ModelName { get; set; }
        [NotMapped]
        public string StatusName { get; set; }


    }
}
