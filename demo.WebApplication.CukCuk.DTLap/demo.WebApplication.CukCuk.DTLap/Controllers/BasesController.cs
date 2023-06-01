using demo.WebApplication.CukCuk.BL.BaseBL;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using demo.WebApplication.CukCuk.DTLap.Common;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Reflection;

namespace demo.WebApplication.CukCuk.DTLap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        [HttpGet]
        public IActionResult GetAllRecord()
        {
            try
            {

                var res = _baseBL.GetAllRecords();

                //Xử lý kết quả trả về từ Tầng BL
                if (res != null)
                {
                    //Trả về dữ liệu cho Client
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

        /// <summary>
        /// API thêm mới Một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>StatusCode và thông tin Bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpPost]
        public IActionResult InsertOneRecord([FromBody] T record)
        {
            try
            {
                if (record == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.InValidData,
                        DevMsg = Resource.DevMsg_InValidData,
                        UserMsg = Resource.UserMsg_InValidData,
                        TracdeId = HttpContext.TraceIdentifier,
                        MoreInfo = "https://"
                    });
                }

                var res = _baseBL.InsertOneRecord(record);

                //Xử lý kết quả trả về từ Tầng BL
                if (res.IsSuccess == true)
                {
                    //Trả về dữ liệu cho Client
                    return StatusCode(StatusCodes.Status201Created, res.Data);
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
        /// API Sửa thông tin một nhân viên theo mã Id
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>StatausCode và id nhân viên vừa xóa</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpPut]
        public IActionResult UpdateRecordById([FromBody] T record)
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

                var res = _baseBL.UpdateRecordById(record);

                //Xử lí kết quả trả về
                if (res.IsSuccess == true)
                {
                    return StatusCode(200, res);
                }

                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, res);
                }
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
        /// API Lấy thông tin chi tiết một bản ghi
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Đối tượng bản ghi muốn lấy</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpGet("{recordId}")]
        public IActionResult GetRecordById([FromRoute] Guid recordId)
        {

            //Chuẩn bị tên stored
            try
            {
                var record = _baseBL.GetRecordById(recordId);

                //Xử lý kết quả trả về

                //Thành công
                if (record != null)
                {
                    return StatusCode(200, record);

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
            //Try catch

        }

        [HttpDelete]
        public IActionResult DeleteRecordByID([FromQuery] Guid recordId)
        {
            try
            {
                var result = _baseBL.DeleteRecordByID(recordId);

                //Xử lý kết quả trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, recordId);
                }

                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, result);
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
