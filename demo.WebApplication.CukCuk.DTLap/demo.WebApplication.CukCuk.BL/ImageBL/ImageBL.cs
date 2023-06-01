using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.DL.ImageDL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.ImageBL
{
    public class ImageBL : IImageBL
    {
        private IImageDL _ImageDL;

        public ImageBL(IImageDL ImageDL)
        {
            _ImageDL = ImageDL;
        }

        /// <summary>
        /// Hàm thêm mới một ảnh
        /// </summary>
        /// <param name="file">ảnh</param>
        /// <param name="path">đường dẫn</param>
        /// <returns></returns>
        public Guid InsertOneImage(IFormFile file, string path)
        {
            if (file != null && path != null)
            {
                if (file.Length > 0)
                {
                    Guid id = Guid.NewGuid();
                    if (file.FileName != null)
                    {
                        int lastIndex = file.FileName.LastIndexOf('.');
                        string extension = file.FileName.Substring(lastIndex);
                        if (!Directory.Exists(path + "\\image"))
                        {
                            Directory.CreateDirectory(path + "\\image");
                        }
                        using (FileStream filestream = System.IO.File.Create(path + "\\image\\" + id + extension))
                        {
                            file.CopyTo(filestream);
                            filestream.Flush();
                            //  return "\\Upload\\" + objFile.files.FileName;
                        }

                        var result = _ImageDL.InsertOneImage(id, extension, "image");
                        if (result != Guid.Empty)
                        {
                            return id;
                        }
                        return Guid.Empty; 
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }
                return Guid.Empty; 
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        public Guid DeleteOneImage(Guid id, string path)
        {
            if (id != Guid.Empty && path != "")
            {
                Image image = _ImageDL.GetImageByFoodID(id);
                System.IO.File.Delete(path + "\\" + image.ImageLocation + "\\" + id + image.ImageType);
                var result = _ImageDL.DeleteOneImage(id);
                if (result != Guid.Empty)
                {
                    return id;
                }
                return Guid.Empty;
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Hàm lấy ảnh theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImageByFoodID(Guid id)
        {
            return _ImageDL.GetImageByFoodID(id);
        }

    }
}
