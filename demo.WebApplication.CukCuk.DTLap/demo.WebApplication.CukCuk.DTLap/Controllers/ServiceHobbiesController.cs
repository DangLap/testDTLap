using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.BL.FoodBL;
using demo.WebApplication.CukCuk.BL.ServiceHobbieBL;
using demo.WebApplication.CukCuk.DTLap.Common;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo.WebApplication.CukCuk.DTLap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHobbiesController : BasesController<ServiceHobbie>
    {
        #region Field
        private IServiceHobbieBL _serviceHobbieBL;
        #endregion

        public ServiceHobbiesController(IServiceHobbieBL serviceHobbieBL) : base(serviceHobbieBL)
        {
            _serviceHobbieBL = serviceHobbieBL;
        }

        /// <summary>
        /// API Lấy thông tin chi tiết một bản ghi
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Đối tượng bản ghi muốn lấy</returns>
        /// Created by: DTLap(09/03/2023)
        [HttpGet("service/{recordFoodId}")]
        public IActionResult GetRecordsByID([FromRoute] Guid recordFoodId)
        {

            //Chuẩn bị tên stored
            try
            {
                var record = _serviceHobbieBL.GetRecordsByID(recordFoodId);

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
    }
}
