using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        #region Method
        public List<T> GetAllRecords()
        {
            return _baseDL.GetAllRecord();
        }

        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public ServiceResult InsertOneRecord(T record)
        {
            if (record != null)
            {
                //validate dữ liệu, những trường không được để trống
                var valdateFailures = ValidateRequestData(record);

                if (valdateFailures.Count > 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = "Invalid data",
                        Data = valdateFailures,
                    };
                }

                var result = _baseDL.InsertOneRecord(record);
                if (result != Guid.Empty)
                {
                    return new ServiceResult
                    {
                        IsSuccess = true,
                        Message = "Success",
                        Data = result,
                    };
                }

                return new ServiceResult(); 
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        /// <summary>
        /// API Sửa thông tin một Bản ghi theo mã Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatausCode và id bản ghi vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        public ServiceResult UpdateRecordById(T record)
        {
            if (record != null)
            {
                //validate dữ liệu, những trường không được để trống
                var valdateFailures = ValidateRequestData(record);

                if (valdateFailures.Count > 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = "Invalid data",
                        Data = valdateFailures,
                    };
                }

                var result = _baseDL.UpdateRecordById(record);
                if (result > 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = true,
                        Message = "Success",
                        Data = result,
                    };
                }

                return new ServiceResult(); 
            }
            else { throw new InvalidDataException(); }
        }

        public virtual int AffterInsert(T record)
        {
            return 0;
        }

        /// <summary>
        /// Hàm trả về bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Dữ liệu bản ghi</returns>
        public T GetRecordById(Guid recordId)
        {
            return _baseDL.GetRecordById(recordId);
        }

        /// <summary>
        /// Hàm xóa bản ghi theo ID
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// Created by: DTLap(09/03/2023)
        public int DeleteRecordByID(Guid recordId)
        {
            return _baseDL.DeleteRecordByID(recordId);
        }

        /// <summary>
        /// Hàm dùng để validate dữ liệu chung
        /// </summary>
        /// <param name="record"></param>
        /// <returns>trường thông tin bị lỗi</returns>
        public List<string> ValidateRequestData(T record)
        {
            var valdateFailures = new List<string>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var properyValue = property.GetValue(record);
                var requiredAttribute = (RequiredAttribute?)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

                if (requiredAttribute != null && String.IsNullOrEmpty(properyValue?.ToString()))
                {
                    valdateFailures.Add(propertyName);
                }
            }

            return valdateFailures;
        }

        #endregion

    }
}
