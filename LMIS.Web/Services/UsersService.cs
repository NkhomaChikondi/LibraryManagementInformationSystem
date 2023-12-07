using LMIS.Web.DTOs.User;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LMIS.Web.Services
{
    public class UsersService : IUserService
    {
        private HttpClient httpClient;
        private HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7258") });
        public async Task<List<User>> GetAllUsers(string token)
        {
            List<User> users = new List<User>();
           
            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync("api/user/GetAllAsync");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of users
                    users = await response.Content.ReadFromJsonAsync<List<User>>();


                }
                
            }
            catch (Exception ex)
            {

                return users;

            }

            return users;
        }

        public async Task<List<Role>> GetAllRoles(string token)
        {
            List<Role> roles = new List<Role>();

            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync("api/Role/GetAllAsync");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of users
                    roles = await response.Content.ReadFromJsonAsync<List<Role>>();


                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {

                return roles;

            }

            return roles;
        }

        public async Task<bool> CreateUser(UserDTO userDTO)
        {
            try
            {

                //create a dynamic user input object


                //serialize user input

                var json = JsonConvert.SerializeObject(userDTO);

                var httpContent = new StringContent(json, Encoding.UTF8);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await HttpClient.PostAsync("api/user/createUser", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {

                    var errorMessage = await response.Content.ReadAsStringAsync();

                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> DeleteUser(int id, string token)
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.DeleteAsync("api/user/Delete/"+id);

                if (response.IsSuccessStatusCode)
                {
                    return true;


                }

            }
            catch (Exception ex)
            {

                return false;

            }

            return false;
        }
    }
}
