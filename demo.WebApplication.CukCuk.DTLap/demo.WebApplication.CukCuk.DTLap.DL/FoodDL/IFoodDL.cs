using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.FoodDL
{
    public interface IFoodDL : IBaseDL<Food>
    {
        public PagingResult GetPagingAndFilter(FilterAndSortObject filterAndSortObject, int pageSize, int pageNumber);

        public Food GetFoodByCode(string code);

        public List<Food> GetAllFood();
    }
}
