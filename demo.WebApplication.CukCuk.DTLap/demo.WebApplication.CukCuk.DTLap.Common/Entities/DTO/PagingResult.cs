using demo.WebApplication.CukCuk.DTLap.Common.Entities;

namespace Demo.WebApplication.Common.Entities.DTO
{
    public class PagingResult
    {
        public List<Food> Data { get; set; }

        public long TotalRecord  { get; set; }
    }
}
