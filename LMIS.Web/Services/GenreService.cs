using LMIS.Web.DTOs.Genre;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LMIS.Web.Services
{
    public class GenreService : IGenreService
    {
        private readonly HttpClient httpClient;

        public GenreService()
        {
            // Initialize HttpClient with base address
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:32782/") 
            };
        }

        public async Task<List<Genre>> GetAllGenres(string token)
        {
            List<Genre> genres = new List<Genre>();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync("api/Genre/GetAllAsync");

                if (response.IsSuccessStatusCode)
                {
                    genres = await response.Content.ReadFromJsonAsync<List<Genre>>();
                }
                else
                {
                    // Handle error response if needed
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }

            return genres;
        }

        public async Task<Genre> GetGenre(int id, string token)
        {
            Genre genre = null;

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"api/Genre/GetGenreById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string serverContent = await response.Content.ReadAsStringAsync();

                     genre = await response.Content.ReadFromJsonAsync<Genre>();
                    
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
            }

            return genre;
        }

        public async Task<bool> CreateGenre(GenreDTO genreDTO, string token)
        {
            try
            {
                var json = JsonConvert.SerializeObject(genreDTO);
                var httpContent = new StringContent(json, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("api/Genre/CreateGenre", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
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

        public async Task<bool> UpdateGenre(GenreDTO genreDTO, string token)
        {
            try
            {
                var json = JsonConvert.SerializeObject(genreDTO);
                var httpContent = new StringContent(json, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PutAsync($"api/Genre/Update/{genreDTO.GenreId}", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string result = await response.Content.ReadAsStringAsync();
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

        public async Task<bool> DeleteGenre(int id, string token)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.DeleteAsync($"api/Genre/Delete/{id}");

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
