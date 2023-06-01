using demo.WebApplication.CukCuk.DTLap.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO
{
    public class SortObject
    {
        public string propertyName {  get; set; }

        public Operator sortKey { get; set; }
    }
}
