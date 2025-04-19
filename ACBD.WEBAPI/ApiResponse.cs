namespace ACBD.WEBAPI
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
}
