using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.ImageBL
{
    public interface IImageBL
    {
        /// <summary>
        /// Hàm thêm mới một ảnh
        /// </summary>
        /// <param name="file">ảnh</param>
        /// <param name="path">đường dẫn</param>
        /// <returns></returns>
        public Guid InsertOneImage(IFormFile file, string path);

        public Guid DeleteOneImage( Guid id , string path );

        /// <summary>
        /// Hàm trả về ảnh theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImageByFoodID(Guid id);

    }
}
