namespace LMIS.Web.Models
{
    public class AuthResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public UserInfo User { get; set; }
    }
}
