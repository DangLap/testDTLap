using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.BL.FoodCustomBL;
using demo.WebApplication.CukCuk.DTLap.Common;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace demo.WebApplication.CukCuk.DTLap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCustomsController : BasesController<FoodCustom>
    {
        #region Field
        private IFoodCustomBL _foodCustomBL;
        #endregion

        public FoodCustomsController(IFoodCustomBL foodCustomBL) : base(foodCustomBL)
        {
            _foodCustomBL = foodCustomBL;
        }

        /// <summary>
        /// API thêm mới Một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatusCode và thông tin Bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpPost("food-custom")]
        public IActionResult InsertOneRecord([FromBody] FoodCustom record)
        {
            try
            {
                if (record == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.DupplicateKey,
                        DevMsg = Resource.DevMsg_InValidData,
                        UserMsg = Resource.UserMsg_InValidData,
                        TracdeId = HttpContext.TraceIdentifier,
                        MoreInfo = "https://"
                    });
                }
                var res = _foodCustomBL.InsertOneFoodCustom(record);

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
            catch (MySqlException mySqlException)
            {
                //if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                //{
                //    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                //    {
                //        ErrorCode = ErrorCode.DupplicateKey,
                //        DevMsg = Resource.DevMsg_DuplicateKey,
                //        UserMsg = Resource.UserMsg_DuplicateKey,
                //        TracdeId = HttpContext.TraceIdentifier,
                //        MoreInfo = "https://"
                //    });
                //}
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.NotAccess,
                    DevMsg = Resource.DevMsg_NotAccess,
                    UserMsg = Resource.User_NotAccess,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
            }
            catch (InvalidDataException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.DupplicateKey,
                    DevMsg = Resource.DevMsg_InValidData,
                    UserMsg = Resource.UserMsg_InValidData,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
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
        /// API Sửa Một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatusCode và thông tin Bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpPut("food-custom")]
        public IActionResult UpadteOneRecord([FromBody] FoodCustom record)
        {
            try
            {
                if (record == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.DupplicateKey,
                        DevMsg = Resource.DevMsg_InValidData,
                        UserMsg = Resource.UserMsg_InValidData,
                        TracdeId = HttpContext.TraceIdentifier,
                        MoreInfo = "https://"
                    });
                }
                var res = _foodCustomBL.UpdateOneFoodCustom(record);

                //Xử lý kết quả trả về từ Tầng BL
                if (res > 0)
                {
                    //Trả về dữ liệu cho Client
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, res);
                }
            }
            catch (MySqlException mySqlException)
            {
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.DupplicateKey,
                        DevMsg = Resource.DevMsg_DuplicateKey,
                        UserMsg = Resource.UserMsg_DuplicateKey,
                        TracdeId = HttpContext.TraceIdentifier,
                        MoreInfo = "https://"
                    });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.NotAccess,
                    DevMsg = Resource.DevMsg_NotAccess,
                    UserMsg = Resource.User_NotAccess,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
            }
            catch (InvalidDataException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.DupplicateKey,
                    DevMsg = Resource.DevMsg_InValidData,
                    UserMsg = Resource.UserMsg_InValidData,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
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
