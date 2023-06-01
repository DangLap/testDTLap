using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.ServiceHobbieBL
{
    public interface IServiceHobbieBL : IBaseBL<ServiceHobbie>
    {
        public List<ServiceHobbie> GetRecordsByID(Guid id);
    }
}
