using Dapper;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.ImageDL
{
    public class ImageDL : BaseDL<Image>, IImageDL
    {
        /// <summary>
        /// Hàm xóa một ảnh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Guid DeleteOneImage(Guid id)
        {
            var dbconnection = GetOpenConnection();
            try
            {
                //chuẩn bị tên stored:
                string storedProcedure = "proc_DeleteOneRecord_Image";

                //Chuẩn bị tham số đầu vào
                var parameter = new DynamicParameters();

                parameter.Add("v_ImageID", id);

                //Thực hiện câu lệnh excecute

                int result = Execute(dbconnection, storedProcedure, parameter, commandType: System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    return id;
                }

                return Guid.Empty;
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

        /// <summary>
        /// Hàm lấy thông tin chi tiết 1 ảnh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImageByFoodID(Guid id)
        {
            var dbconnection = GetOpenConnection();

            try
            {
                //chuẩn bị tên storedProcedure
                string storeProcedureName = "proc_GetRecordByID_Image";

                //chuẩn bị tham số đầu vào:
                var parameter = new DynamicParameters();
                parameter.Add("v_ImageID", id);



                //Thực hiện câu lệnh storedProcedure
                var result = QueryFirstOrDefault(dbconnection, storeProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);

                return result;
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

        /// <summary>
        /// Hàm thêm mới một ảnh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="extension"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Guid InsertOneImage(Guid id, string extension, string path)
        {
            var dbconnection = GetOpenConnection();
            try
            {
                //Chuẩn bị tên stored Procedure
                string storedProcedureName = "proc_InsertOneRecord_Image";

                //Chuẩn bị tham số đầu vào:
                var parameter = new DynamicParameters();
                parameter.Add("v_imageID", id);
                parameter.Add("v_imageType", extension);
                parameter.Add("v_imageLocation", path);

                //khởi tạo kết nối với database:

                //thực hiện câu lệnh sql
                int numberOfAffectedRows = Execute(dbconnection, storedProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return id;
                }

                return Guid.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dbconnection.Close();
            }
        }
    }
}
