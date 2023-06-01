using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace demo.WebApplication.CukCuk.DTLap.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Hàm mở kết nối với database
        /// </summary>
        /// <returns>Kết nối được mở</returns>
        public IDbConnection GetOpenConnection();

        /// <summary>
        /// Hàm trả về dòng đầu tiên bị ảnh hưởng khi thao thác với database
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>T</returns>
        /// 
        public T QueryFirstOrDefault(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm của Dapper trả về kiểu dữ liệu là gridReader, thường dùng để lấy theo một kiểu dữ liệu được custom
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// Created by: DTLap(09/03/2023)
        public GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm execure để trả về số bản ghi bị ảnh hưởng khi thực hiện câu lệnh sql
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// Created by: DTLap(09/03/2023)
        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        public List<T> GetAllRecord();

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public Guid InsertOneRecord(T record);

        public Guid InsertOneRecord(T record, IDbTransaction transaction, IDbConnection dbConnection);

        /// <summary>
        /// API Sửa thông tin một Bản ghi theo mã Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatausCode và id bản ghi vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        public int UpdateRecordById(T record, IDbTransaction transaction, IDbConnection dbConnection);

        /// <summary>
        /// API Sửa thông tin một Bản ghi theo mã Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatausCode và id bản ghi vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        public int UpdateRecordById(T record);

        /// <summary>
        /// Hàm trả về bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Dữ liệu bản ghi</returns>
        /// Created by: DTLap(09/03/2023)
        public T GetRecordById(Guid recordId);

        /// <summary>
        /// Hàm xóa bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Created by: DTLap(09/03/2023)
        public int DeleteRecordByID(Guid recordId);

    }
}
