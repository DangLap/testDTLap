using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using demo.WebApplication.CukCuk.DTLap.DL.ServiceHobbieDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.ServiceHobbieBL
{
    public class ServiceHobbieBL : BaseBL<ServiceHobbie>, IServiceHobbieBL
    {
        #region Field

        private IServiceHobbieDL _serviceHobbieDL;

        #endregion

        #region Constructor

        public ServiceHobbieBL(IServiceHobbieDL serviceHobbieDL) : base(serviceHobbieDL)
        {
            _serviceHobbieDL = serviceHobbieDL;
        }
        #endregion

        public List<ServiceHobbie> GetRecordsByID(Guid id)
        {
            return _serviceHobbieDL.GetRecordsByID(id);
        }

    }
}
