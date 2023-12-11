using LMIS.Web.DTOs.Role;
using LMIS.Web.DTOs.User;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LMIS.Web.Services
{
    public class RolesService : IroleService
    {
        private HttpClient httpClient;

        private HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:32784/") });

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

                    // Deserialize the JSON response into a list of roles
                    roles = await response.Content.ReadFromJsonAsync<List<Role>>();
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    // Handle error response if needed
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }

            return roles;
        }

        public async Task<Role> GetRole(int id, string token)
        {
            Role role = null;

            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync($"api/Role/GetRoleById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a concrete role class
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

                    role = System.Text.Json.JsonSerializer.Deserialize<Role>(apiResponse.Result);

                    return role;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }

            return role;
        }

        public async Task<bool> CreateRole(RoleDTO roleDTO, string token)
        {
            try
            {
                // Serialize role input
                var json = JsonConvert.SerializeObject(roleDTO);
                var httpContent = new StringContent(json, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                // Send POST request to create a role
                var response = await HttpClient.PostAsync("api/Role/CreateRole", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    // Handle error response if needed
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                return false;
            }
        }

        public async Task<bool> UpdateRole(RoleDTO roleDTO, string token)
        {
            try
            {
                // Serialize role input
                var json = JsonConvert.SerializeObject(roleDTO);
                var httpContent = new StringContent(json, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Set authorization header
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send PUT request to update a role
                var response = await HttpClient.PutAsync($"api/Role/UpdateRole/{roleDTO.RoleId}", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    // Handle error response if needed
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                return false;
            }
        }

        public async Task<bool> DeleteRole(int id, string token)
        {
            try
            {
                // Set authorization header
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send DELETE request to delete a role
                var response = await HttpClient.DeleteAsync($"api/Role/DeleteRole/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }

            return false;
        }
    }

}
