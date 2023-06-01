using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.FoodBL
{
    public interface IFoodBL : IBaseBL<Food>
    {
        public PagingResult GetPagingAndFilter(FilterAndSortObject filterAndSort, int pageSize, int pageNumber);

        public string GetFoodCode(string code);

        public int CheckDupplicateCode(string code);

        public List<Food> GetAllFood();
    }
}
