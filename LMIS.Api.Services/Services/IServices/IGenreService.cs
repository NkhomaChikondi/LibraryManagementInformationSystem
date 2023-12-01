using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IGenreService
    {
        Task<GenreDTO> CreateGenre(GenreDTO genre, string userIdClaim);
        Task DeleteGenreAsync(int genreId);
        IEnumerable<GenreDTO> GetAllGenres();
        Task<GenreDTO> GetGenreByIdAsync(int genreId);
        Task<GenreDTO> UpdateGenreAsync(GenreDTO genre, int Id);
    }
}