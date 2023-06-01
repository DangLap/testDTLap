using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.FoodCustomDL
{
    public interface IFoodCustomDL : IBaseDL<FoodCustom>
    {
        public int InsertOneFoodCustomRecord(FoodCustom foodCustom);

    }
}
