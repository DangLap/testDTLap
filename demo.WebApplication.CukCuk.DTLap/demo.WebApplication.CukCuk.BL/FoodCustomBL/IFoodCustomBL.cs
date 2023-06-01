using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.FoodCustomBL
{
    public interface IFoodCustomBL : IBaseBL<FoodCustom>
    {
        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public Guid InsertOneFoodCustom(FoodCustom record);

        /// <summary>
        /// Hàm Sửa một một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public int UpdateOneFoodCustom(FoodCustom record);
    }
}
