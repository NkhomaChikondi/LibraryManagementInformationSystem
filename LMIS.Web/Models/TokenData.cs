namespace LMIS.Web.Models
{
    public class TokenData
    {

        public string UserId { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string TokenType { get; set; }
        public DateTime TokenExpiryMinutes { get; set; }
    }
}
