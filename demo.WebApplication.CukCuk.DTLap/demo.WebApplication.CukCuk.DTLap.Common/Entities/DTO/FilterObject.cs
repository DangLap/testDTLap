using demo.WebApplication.CukCuk.DTLap.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO
{
    public class FilterObject
    {
        public string Property { get; set; }

        public PropertyType PropertyType { get; set; }

        public Operator Operator { get; set; }

        public object Value { get; set; }

    }
}
