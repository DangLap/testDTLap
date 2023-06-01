using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.DL.ImageDL
{
    public interface IImageDL
    {
        /// <summary>
        /// Hàm thêm mới một ảnh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="extension"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Guid InsertOneImage(Guid id, string extension, string path);

        /// <summary>
        /// Hàm lấy thông tin chi tiết 1 ảnh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImageByFoodID(Guid id);

        /// <summary>
        /// Hàm xóa 1 ảnh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Guid DeleteOneImage(Guid id);

    }
}
