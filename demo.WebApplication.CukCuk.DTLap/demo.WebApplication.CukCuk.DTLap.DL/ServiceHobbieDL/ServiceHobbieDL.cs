using Dapper;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.ServiceHobbieDL
{
    public class ServiceHobbieDL : BaseDL<ServiceHobbie>, IServiceHobbieDL
    {
        /// <summary>
        /// Hàm xóa các bản ghi theo ID món ăn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dbConnection"></param>
        /// <param name="Transaction"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author: DTLap
        public int DeleteRecordsByFoodID(Guid id, IDbConnection dbConnection, IDbTransaction Transaction)
        {
            //Chuẩn bị tên stored procedure 
            string storedProcedureName = "proc_DeleteByFoodID_ServiceHobbie";

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("v_FoodID", id);

            //Thực hiện chạy câu lệnh stored Procedure với tham số đầu vào ở trên
            int numberOfAffectedRows = Execute(dbConnection, storedProcedureName, parameter, transaction: Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm lấy các sở thích phục vụ theo ID món ăn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: DTLap
        public List<ServiceHobbie> GetRecordsByID(Guid id)
        {
            //Khởi tạo kết nối với db
            var dbConnection = GetOpenConnection();

            try
            {
                //Chuẩn bị tên stored
                string storedProcedureName = $"proc_GetRecordsByID_ServiceHobbie";

                //Chuẩn bị tham số đầu vào
                var parameter = new DynamicParameters();
                parameter.Add($"v_FoodID", id);

                //Thực hiện câu lệnh sql
                var multipleResult = dbConnection.Query<ServiceHobbie>(storedProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);

                if (multipleResult != null)
                {
                    //var listServiceHobbies = multipleResult.Read<ServiceHobbie>().ToList();
                    return multipleResult.ToList();
                }
                else return new List<ServiceHobbie>();
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


    }
}
