using LMIS.Web.DTOs.Genre;
using LMIS.Web.Models;

namespace LMIS.Web.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllGenres(string token);
        Task<Genre> GetGenre(int id, string token);
        Task<bool> CreateGenre(GenreDTO genreDTO, string token);
        Task<bool> UpdateGenre(GenreDTO genreDTO, string token);
        Task<bool> DeleteGenre(int id, string token);
    }

}
