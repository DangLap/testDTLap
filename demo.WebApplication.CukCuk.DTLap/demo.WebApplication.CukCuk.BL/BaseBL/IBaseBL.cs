using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        public List<T> GetAllRecords();

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public ServiceResult InsertOneRecord(T record);

        /// <summary>
        /// API Sửa thông tin một Bản ghi theo mã Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatausCode và id bản ghi vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        public ServiceResult UpdateRecordById(T record);

        /// <summary>
        /// Hàm trả về bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Dữ liệu bản ghi</returns>
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
