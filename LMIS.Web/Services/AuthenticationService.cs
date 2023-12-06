using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using LMIS.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace LMIS.Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private HttpClient httpClient;
        private HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7258") });
        public AuthenticationService(IConfiguration configuration)
        {
            this._configuration = _configuration;
        }
        public async Task<AuthResult> Login(string email,string password)
        {
            try
            {

                //create a dynamic user input object
                var userInput = new { Email = email, Password = password };

                //serialize user input

                var json = JsonConvert.SerializeObject(userInput);

                var httpContent = new StringContent(json, Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await HttpClient.PostAsync("api/auth/login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = (JObject)JsonConvert.DeserializeObject(content);

                    var token = data["token"].Value<string>();
                    var userId = data["userId"].Value<string>();
                    var roleName = data["role"].Value<string>();
                    var username = data["username"].Value<string>();
                    var firstName = data["firstName"].Value<string>();
                    var lastName = data["lastName"].Value<string>();



                    return new AuthResult
                    {
                        Status = "success",
                        User = new UserInfo
                        {
                            Token = token,
                            UserId = userId,
                            RoleName = roleName,
                            Username = username,
                            FirstName = firstName,
                            LastName = lastName
                        }
                    };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new AuthResult { Status = "failed", Message = errorMessage };
                }
            }
            catch (Exception ex)
            {

                return new AuthResult { Status = "failed", Message = $"We could not connect you to the website. Contact the admin or try again later. {ex.Message}" };
            }
        }
    }
}
