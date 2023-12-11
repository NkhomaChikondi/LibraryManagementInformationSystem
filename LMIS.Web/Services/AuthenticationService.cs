using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using LMIS.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LMIS.Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
       
        
        private readonly HttpClient httpClient;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Initialize HttpClient with base address
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:32784/")
            };
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

                var response = await httpClient.PostAsync("api/auth/login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    TokenData tokenData = await response.Content.ReadFromJsonAsync<TokenData>();

                    var token = tokenData.Token;
                    var userId = tokenData.UserId;
                    var roleName = tokenData.Role;
                    var firstName = tokenData.FirstName;
                    var lastName = tokenData.LastName; 



                    return new AuthResult
                    {
                        Status = "success",
                        User = new UserInfo
                        {
                            Token = token,
                            UserId = userId,
                            RoleName = roleName,
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
