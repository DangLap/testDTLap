using Demo.WebApplication.Common.Enums;

namespace Demo.WebApplication.Common.Entities.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        public string? DevMsg { get; set; }

        public string? UserMsg { get; set; }

        public object? TracdeId { get; set; }

        public string? MoreInfo { get; set; }


    }
}
