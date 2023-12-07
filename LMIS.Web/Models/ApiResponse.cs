namespace LMIS.Web.Models
{
    public class ApiResponse
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
    }
}
