namespace Demo.WebApplication.Common.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Ngoại lệ
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        InValidData = 2,

        /// <summary>
        /// Trùng mã
        /// </summary>
        DupplicateKey = 3,

        /// <summary>
        /// Không thực hiện được thao tác
        /// </summary>
        NotAccess = 4,
    }
}
