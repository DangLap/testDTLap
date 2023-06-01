using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using demo.WebApplication.CukCuk.DTLap.DL.FoodDL;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.FoodBL
{
    public class FoodBL : BaseBL<Food>, IFoodBL
    {
        #region Field

        private IFoodDL _foodDL;

        #endregion

        #region Constructor

        public FoodBL(IFoodDL foodDL) : base(foodDL)
        {
            _foodDL = foodDL;
        }

        #endregion

        /// <summary>
        /// Hàm lấy mã món ăn
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetFoodCode(string code)
        {
            string newCode = RemoveDiacritics(code).Replace(" ", "").ToUpper();
            {
                var res = _foodDL.GetFoodByCode(newCode);
                if (res == null)
                {
                    return newCode;
                }
                else
                {
                    string[] words = code.Split(' '); // Tách chuỗi thành các từ
                    newCode = "";
                    int count = 1;
                    foreach (string word in words)
                    {
                        char firstChar = word[0]; // Lấy kí tự đầu tiên của từ
                        newCode += firstChar;
                    }
                    newCode = RemoveDiacritics(newCode);
                    newCode = newCode.ToUpper();
                    res = _foodDL.GetFoodByCode(newCode);
                    if (res == null)
                    {
                        return newCode;
                    }
                    else
                    {
                        string result = "";
                        do
                        {
                            if (count < 10)
                            {
                                result = newCode + "0" + count.ToString();
                                count++;
                            }
                            else
                            {
                                result = newCode + count.ToString();
                                count++;
                            }
                        }
                        while (_foodDL.GetFoodByCode(result) != null);
                        return result;
                    }
            }
            
            
            }
            
        }

        /// <summary>
        /// Hàm kiểm tra trùng mã món ăn
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int CheckDupplicateCode(string code)
        {
            var res = _foodDL.GetFoodByCode(code);
            if (res != null)
            {
                return 1;
            }
            else { return 0; }
        }

        /// <summary>
        /// Hàm lấy dữ liệu theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PagingResult GetPagingAndFilter(FilterAndSortObject filterAndSort, int pageSize, int pageNumber)
        {
            return _foodDL.GetPagingAndFilter(filterAndSort, pageSize, pageNumber);
        }

        /// <summary>
        /// Hàm loại bỏ các kí tự Việt Nam, và các kí tự có dấu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public List<Food> GetAllFood()
        {
            return _foodDL.GetAllFood();
        }
    }
}
