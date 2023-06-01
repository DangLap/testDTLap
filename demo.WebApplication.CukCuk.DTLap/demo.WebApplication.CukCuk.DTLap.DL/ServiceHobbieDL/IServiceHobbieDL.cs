using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.ServiceHobbieDL
{
    public interface IServiceHobbieDL : IBaseDL<ServiceHobbie>
    {
        public List<ServiceHobbie> GetRecordsByID(Guid id);

        public int DeleteRecordsByFoodID(Guid id, IDbConnection dbConnection, IDbTransaction Transaction);
    }
}
