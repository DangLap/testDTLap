using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.Common.Entities
{
    public class ServiceHobbie
    {
        [Key]
        public Guid ServiceHobbieID { get; set; }

        public string ServiceHobbieName { get; set; }

        public int AdditionalCost { get; set; }

        public Guid FoodID { get; set; }
    }
}
