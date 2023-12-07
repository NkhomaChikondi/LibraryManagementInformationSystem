using LMIS.Web.DTOs.Member;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LMIS.Web.Services
{
    public class MemberService : IMemberService
    {
        private HttpClient httpClient;

        private HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7258") });

        public async Task<List<Member>> GetAllMembers(string token)
        {
            List<Member> members = new List<Member>();

            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync("api/Member/GetAllAsync");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of members
                    members = await response.Content.ReadFromJsonAsync<List<Member>>();
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

            return members;
        }

        public async Task<Member> GetMember(int id, string token)
        {
            Member member = null;

            try
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await HttpClient.GetAsync($"api/Member/GetMemberById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a concrete member class
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

                    member = System.Text.Json.JsonSerializer.Deserialize<Member>(apiResponse.Result);

                    return member;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }

            return member;
        }

        public async Task<bool> CreateMember(MemberDTO memberDTO, string token)
        {
            try
            {
                // Serialize member input
                var json = JsonConvert.SerializeObject(memberDTO);
                var httpContent = new StringContent(json, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send POST request to create a member
                var response = await HttpClient.PostAsync("api/Member/CreateMember", httpContent);

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

        public async Task<bool> UpdateMember(MemberDTO memberDTO, string token)
        {
            try
            {
                // Serialize member input
                var json = JsonConvert.SerializeObject(memberDTO);
                var httpContent = new StringContent(json, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Set authorization header
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send PUT request to update a member
                var response = await HttpClient.PutAsync($"api/Member/UpdateMember/{memberDTO.MemberId}", httpContent);

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

        public async Task<bool> DeleteMember(int id, string token)
        {
            try
            {
                // Set authorization header
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send DELETE request to delete a member
                var response = await HttpClient.DeleteAsync($"api/Member/DeleteMember/{id}");

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
