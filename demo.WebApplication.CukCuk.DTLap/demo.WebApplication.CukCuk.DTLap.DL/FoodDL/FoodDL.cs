using Dapper;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.FoodDL
{
    public class FoodDL : BaseDL<Food>, IFoodDL
    {
        public Food GetFoodByCode(string code)
        {
            //Khởi tạo kết nối với database:
            var dbconnection = GetOpenConnection();
            try
            {

                //Chuẩn bị tham số đầu vào
                var parameter = new DynamicParameters();
                parameter.Add("v_FoodCode", code);

                //Chuẩn bị tên storedProcedure 
                string storedProcedureName = "get_FoodCode_List";



                //Chạy câu lệnh execute
                var res = QueryFirstOrDefault(dbconnection, storedProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dbconnection.Close();
                dbconnection.Dispose();
            }
        }

        public PagingResult GetPagingAndFilter(FilterAndSortObject filterAndSortObject, int pageSize, int pageNumber)
        {
            var dbConnection = GetOpenConnection();
            try
            {
                //Chuẩn bị tên storedProcedure
                string storedProcName = "Proc_GetPaging_Food";

                //Chuẩn bị tham số đầu vào cho stored Procedure
                var parameter = new DynamicParameters();
                var listCondition = new List<string>();
                string whereClause = "";

                if (filterAndSortObject.Filters.Count > 0)
                {
                    for (int i = 0; i < filterAndSortObject.Filters.Count; i++)
                    {
                        if (filterAndSortObject.Filters[i].PropertyType == Common.Enums.PropertyType.Stringtype)
                        {
                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.Equal)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} like '{filterAndSortObject.Filters[i].Value}'");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.Like)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} like '%{filterAndSortObject.Filters[i].Value}%'");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.StartWith)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} like '{filterAndSortObject.Filters[i].Value}%'");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.EndWith)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} like '%{filterAndSortObject.Filters[i].Value}'");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.NotLike)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} not like '%{filterAndSortObject.Filters[i].Value}%'");
                            }
                        }

                        else if (filterAndSortObject.Filters[i].PropertyType == Common.Enums.PropertyType.IntType)
                        {
                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.Equal)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} = {filterAndSortObject.Filters[i].Value}");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.Smaller)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} < {filterAndSortObject.Filters[i].Value}");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.Greater)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} > {filterAndSortObject.Filters[i].Value}");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.SmallerOrEqual)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} <= {filterAndSortObject.Filters[i].Value}");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.GreaterOrEqual)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} >= {filterAndSortObject.Filters[i].Value}");
                            }
                        }

                        else
                        {
                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.True)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} = TRUE");
                            }

                            if (filterAndSortObject.Filters[i].Operator == Common.Enums.Operator.False)
                            {
                                listCondition.Add($"{filterAndSortObject.Filters[i].Property} = FALSE");
                            }
                        }
                    }

                    if (listCondition.Count > 0)
                    {
                        whereClause = $"({string.Join(" AND ", listCondition)})";
                    }
                }

                string sortClause = "";

                if (filterAndSortObject.Sort.propertyName != null && filterAndSortObject.Sort.sortKey != null)
                {
                    sortClause = $"{filterAndSortObject.Sort.propertyName} {filterAndSortObject.Sort.sortKey}";
                }


                parameter.Add("@v_Where", whereClause);
                parameter.Add("@v_Offset", (pageNumber - 1) * pageSize);
                parameter.Add("@v_Limit", pageSize);
                if (sortClause != "")
                {
                    parameter.Add("@v_Sort", sortClause);
                }
                else
                {
                    parameter.Add("@v_Sort", "FoodCode ASC");

                }

                var multipleResult = QueryMultiple(dbConnection, storedProcName, parameter, commandType: System.Data.CommandType.StoredProcedure);

                if (multipleResult != null)
                {
                    var foods = multipleResult.Read<Food>().ToList();
                    var totalCount = multipleResult.Read<long>().Single();
                    return new PagingResult()
                    {
                        Data = foods,
                        TotalRecord = totalCount
                    };
                }
                else return new PagingResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public List<Food> GetAllFood()
        {
            var dbConnection = GetOpenConnection();
            try
            {
                string storedProc = "Select * from Food";
                var result = QueryMultiple(dbConnection, storedProc, commandType: CommandType.Text);
                if (result != null)
                {
                    var foods = result.Read<Food>().ToList();
                    return foods;
                }
                return new List<Food>();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
