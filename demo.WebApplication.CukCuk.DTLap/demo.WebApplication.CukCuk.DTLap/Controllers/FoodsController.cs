using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.BL.FoodBL;
using demo.WebApplication.CukCuk.DTLap.Common;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo.WebApplication.CukCuk.DTLap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : BasesController<Food>
    {
        #region Field
        private IFoodBL _foodBL;
        #endregion

        public FoodsController(IFoodBL foodBL) : base(foodBL)
        {
            _foodBL = foodBL;
        }

        /// <summary>
        /// API lấy các món ăn phân trang kết hợp lọc và tìm kiếm
        /// </summary>
        /// <param name="keyWord">Từ khóa</param>
        /// <param name="pageSize">kích cỡ trang </param>
        /// <param name="pageNumber">số trang</param>
        /// <returns>Danh sách nhân viên và tổng số bản ghi</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpPost("get-paging")]
        public IActionResult GetPaging([FromBody] FilterAndSortObject filterAndSortObject, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            try
            {
                var res = _foodBL.GetPagingAndFilter(filterAndSortObject, pageSize, pageNumber);

                //Xử lý kết quả trả về từ database:
                if (res != null)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exepption,
                    UserMsg = Resource.UserMsg_Exception,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
                ;
            }
        }

        [HttpGet("new-code")]
        public IActionResult GetNewCode([FromQuery] string foodName)
        {
            try
            {
                var res = _foodBL.GetFoodCode(foodName);

                //Xử lý kết quả trả về từ database:
                if (res != null)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResult
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
        /// API kiểm tra mã món ăn có tồn tại hay khônng
        /// </summary>
        /// <param name="foodCode"></param>
        /// <returns></returns>
        [HttpGet("code-dupplicate")]
        public IActionResult CheckDupplicateCode([FromQuery] string foodCode)
        {
            try
            {
                var res = _foodBL.CheckDupplicateCode(foodCode);

                //Xử lý kết quả trả về từ database:
                if (res == 1)
                {
                    return StatusCode(StatusCodes.Status200OK, 1);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, 0);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exepption,
                    UserMsg = Resource.UserMsg_Exception,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
                ;
            }
        }

        /// <summary>
        /// API lấy ra toàn bộ món ăn
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        public IActionResult GetAllFood()
        {
            try
            {
                var res = _foodBL.GetAllFood();

                //Xử lý kết quả trả về từ database:
                if (res != null)
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exepption,
                    UserMsg = Resource.UserMsg_Exception,
                    TracdeId = HttpContext.TraceIdentifier,
                    MoreInfo = "https://"
                });
                ;
            }
        }

    }

}
