using Microsoft.Extensions.Configuration;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IEmailService
    {
        IConfiguration _configuration { get; }

        public string SendMail(string email, string subject, string HtmlMessage);
    }
}