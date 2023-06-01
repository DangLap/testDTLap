using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO
{
    public class FilterAndSortObject
    {
        public List<FilterObject> Filters { get; set; }

        public SortObject Sort { get; set; }
    }
}
