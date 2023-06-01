using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo.WebApplication.CukCuk.DTLap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessedAtsController : BasesController<ProcessedAt>
    {
        public ProcessedAtsController(IBaseBL<ProcessedAt> baseBL) : base(baseBL)
        {
        }
    }
}
