using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.BL.ImageBL;
using demo.WebApplication.CukCuk.DTLap.Common;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.IO;

namespace demo.WebApplication.CukCuk.DTLap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private IImageBL _imageBL;
        public ImagesController(IImageBL imageBL, IWebHostEnvironment environment)
        {
            _environment = environment;
            _imageBL = imageBL;
        }

        /// <summary>
        /// API thêm mới một ảnh
        /// </summary>
        /// <param name="fileUpload">Ảnh thêm mới</param>
        /// <returns>ID ảnh vừa được thêm</returns>
        [HttpPost]
        public IActionResult InsertOneImage([FromForm] FileUpload fileUpload)
        {
            try
            {

                var res = _imageBL.InsertOneImage(fileUpload.ImageFile, _environment.ContentRootPath);

                //Xử lý kết quả trả về từ Tầng BL
                if (res != Guid.Empty)
                {
                    //Trả về dữ liệu cho Client
                    return StatusCode(StatusCodes.Status201Created, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, res);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exepption,
                    UserMsg = Resource.UserMsg_Exception,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
            }
        }

        /// <summary>
        /// API lấy ảnh theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>File ảnh</returns>
        [HttpGet]
        public IActionResult GetImageByFoodID(Guid id)
        {
            //Byte[] b = System.IO.File.ReadAllBytes(_environment.ContentRootPath + "\\image\\" + "79f17f39-af6a-4609-9c02-5d405be39386.png");  
            //return File(b, "image/jpeg");
            try
            {

                var res = _imageBL.GetImageByFoodID(id);

                //Xử lý kết quả trả về từ Tầng BL
                if (res != null)
                {
                    //Trả về dữ liệu cho Client
                    Byte[] b = System.IO.File.ReadAllBytes(_environment.ContentRootPath + res.ImageLocation + "\\" + res.ImageID + res.ImageType);
                    return File(b, "image/jpeg");
                    //return StatusCode(StatusCodes.Status201Created, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, res);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exepption,
                    UserMsg = Resource.UserMsg_Exception,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
            }

        }

        [HttpDelete]
        public IActionResult DeleteImageByID(Guid id)
        {
            try
            {
                var res = _imageBL.DeleteOneImage(id, _environment.ContentRootPath);
                if (res != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, res);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exepption,
                    UserMsg = Resource.UserMsg_Exception,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
            }
        }
    }
}
