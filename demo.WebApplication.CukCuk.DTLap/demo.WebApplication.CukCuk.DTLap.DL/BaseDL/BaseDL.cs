using Dapper;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace demo.WebApplication.CukCuk.DTLap.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field
        private readonly string _connectionString = "Server=localhost ; Port = 3306;Database=misa.cukcuk.dtlap;Uid=root; Pwd = Dangthelap112x;";
        #endregion

        /// <summary>
        /// Hàm mở kết nối với database
        /// </summary>
        /// <returns></returns>
        /// Created by: DTLap(09/03/2023)
        public IDbConnection GetOpenConnection()
        {
            var mySqlConnection = new MySqlConnection(_connectionString);
            mySqlConnection.Open();
            return mySqlConnection;
        }

        /// <summary>
        /// Hàm trả về số dòng bị ảnh hưởng
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// author: DTLap (06/04)
        /// <returns></returns>
        /// Created by: DTLap(09/03/2023)
        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Hàm trả về dòng đầu tiên bị ảnh hưởng khi thao thác với database
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// Created by: DTLap(09/03/2023)
        public T QueryFirstOrDefault(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Hàm trả về kiểu dữ liệu gridreader, dùng để trả về một danh sách bao gồm cả tổng số
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Hàm lấy toàn bộ bản ghi
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllRecord()
        {
            //Khởi tạo kết nối tới database:
            var dbConnection = GetOpenConnection();

            try
            {
                //chuẩn bị tên stored Procedure
                string storedProcedureName = $"Proc_GetAllRecord_{typeof(T).Name}";

                //Khởi tạo parameter



                //thực hiện câu lệnh sql
                var multipleResult = QueryMultiple(dbConnection, storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                if (multipleResult != null)
                {
                    var records = multipleResult.Read<T>().ToList();
                    //var totalCount = multipleResult.Read<long>().Single();
                    return records;
                }
                else return new List<T>();
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

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public Guid InsertOneRecord(T record)
        {
            //Khởi tạo kết nối với database:
            var dbConnection = GetOpenConnection();
            var transaction = dbConnection.BeginTransaction();

            try
            {
                //string sqlQuerry = "select DeparmentName from Department d where d.DepartmentId = " + employee.DepartmentId;
                //Chuẩn bị câu lệnh proc
                string storedProcedureName = $"Proc_InsertOneRecord_{typeof(T).Name}";

                //Chuẩn bị tham số đầu vào cho câu lệnh insert into
                var properties = typeof(T).GetProperties();
                var recordId = Guid.NewGuid();
                var parameters = new DynamicParameters();
                var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
                primaryKeyProperty?.SetValue(record, recordId);
                foreach (var property in properties)
                {
                    string propertyName = $"v_{property.Name}";
                    var propertyValue = property.GetValue(record);
                    parameters.Add(propertyName, propertyValue);
                }

                //Thực hiện gọi vào Database để chạy câu lệnh InsertInto với tham số đầu vào ở trên
                int numberOfAffectedRows = Execute(dbConnection, storedProcedureName, parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                if (numberOfAffectedRows == 0)
                {
                    transaction.Rollback();
                    return Guid.Empty;
                }
                else
                {
                    transaction.Commit();
                    return recordId;
                }

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Hàm thêm mới bản ghi dành cho các DTO đặc biệt
        /// </summary>
        /// <param name="record">bản ghi</param>
        /// <param name="transaction">transaction</param>
        /// <param name="dbConnection">Kết nối database</param>
        /// <returns></returns>
        public Guid InsertOneRecord(T record, IDbTransaction transaction, IDbConnection dbConnection)
        {

            try
            {
                //string sqlQuerry = "select DeparmentName from Department d where d.DepartmentId = " + employee.DepartmentId;
                //Chuẩn bị câu lệnh proc
                string storedProcedureName = $"Proc_InsertOneRecord_{typeof(T).Name}";

                //Chuẩn bị tham số đầu vào cho câu lệnh insert into
                var properties = typeof(T).GetProperties();
                var recordId = Guid.NewGuid();
                var parameters = new DynamicParameters();
                var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
                primaryKeyProperty?.SetValue(record, recordId);
                foreach (var property in properties)
                {
                    string propertyName = $"v_{property.Name}";
                    var propertyValue = property.GetValue(record);
                    parameters.Add(propertyName, propertyValue);
                }
                //Lấy parameter ở stored ra, sau đó tìm ở trong object, rồi mới gán vào parameter

                //Thực hiện gọi vào Database để chạy câu lệnh InsertInto với tham số đầu vào ở trên
                int numberOfAffectedRows = Execute(dbConnection, storedProcedureName, parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                if (numberOfAffectedRows == 0)
                {
                    return Guid.Empty;
                }
                else
                {
                    return recordId;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// API Sửa thông tin một Bản ghi theo mã Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatausCode và id bản ghi vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        public int UpdateRecordById(T record)
        {
            var dbConnection = GetOpenConnection();
            var transaction = dbConnection.BeginTransaction();

            //Chuẩn bị tham số đầu vào cho câu lệnh update
            try
            {
                var properties = typeof(T).GetProperties();
                var parameters = new DynamicParameters();
                foreach (var property in properties)
                {
                    string propertyName = $"v_{property.Name}";
                    var propertyValue = property.GetValue(record);
                    parameters.Add(propertyName, propertyValue);
                }

                //Chuẩn bị tên stored procedure
                string storedProcdureName = $"Proc_UpdateOneRecord_{typeof(T).Name}";


                //thực hiện gọi vào database để chạy câu lệnh stored procedure
                var numberOfAffectedRows = Execute(dbConnection, storedProcdureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                int result = 0;
                if (numberOfAffectedRows > 0)
                {
                    transaction.Commit();
                    result = 1;
                }
                else { transaction.Rollback(); }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
                transaction.Dispose();
            }
        }

        /// <summary>
        /// API Sửa thông tin một Bản ghi theo mã Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatausCode và id bản ghi vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        public int UpdateRecordById(T record, IDbTransaction transaction, IDbConnection dbConnection)
        {
            //Chuẩn bị tham số đầu vào cho câu lệnh update
            try
            {
                var properties = typeof(T).GetProperties();
                var parameters = new DynamicParameters();
                foreach (var property in properties)
                {
                    string propertyName = $"v_{property.Name}";
                    var propertyValue = property.GetValue(record);
                    parameters.Add(propertyName, propertyValue);
                }

                //Chuẩn bị tên stored procedure
                string storedProcdureName = $"Proc_UpdateOneRecord_{typeof(T).Name}";


                //thực hiện gọi vào database để chạy câu lệnh stored procedure
                var numberOfAffectedRows = Execute(dbConnection, storedProcdureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                int result = 0;
                if (numberOfAffectedRows > 0)
                {
                    result = 1;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Hàm trả về bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Dữ liệu bản ghi</returns>
        /// Created by: DTLap(09/03/2023)
        public T GetRecordById(Guid recordId)
        {
            //Khởi tạo kết nối với db
            var dbConnection = GetOpenConnection();

            try
            {
                //Chuẩn bị tên stored
                string storedProcedureName = $"proc_GetRecordByID_{typeof(T).Name}";

                //Chuẩn bị tham số đầu vào
                var parameter = new DynamicParameters();
                parameter.Add($"v_{typeof(T).Name}ID", recordId);



                //Thực hiện câu lệnh sql
                var record = QueryFirstOrDefault(dbConnection, storedProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);

                return record;
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

        /// <summary>
        /// Hàm xóa bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Created by: DTLap(09/03/2023)
        public int DeleteRecordByID(Guid recordId)
        {
            //Mở kết nối với database
            var databaseConnection = GetOpenConnection();

            try
            {
                //Chuẩn bị tên stored Procedure
                string procName = $"proc_DeleteOneRecord_{typeof(T).Name}";

                //Chuẩn  bị tham số đầu vào
                var parameter = new DynamicParameters();
                parameter.Add($"v_{typeof(T).Name}ID", recordId);



                //Thực hiện câu lệnh storedProcedure:
                int numberOfAffectedRows = Execute(databaseConnection, procName, parameter, commandType: CommandType.StoredProcedure);

                return numberOfAffectedRows;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                databaseConnection.Close();
                databaseConnection.Dispose();
            }

        }
    }
}
