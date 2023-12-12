using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IGenreService
    {
        Task<BaseResponse<GenreDTO>> CreateGenre(GenreDTO genre, string userIdClaim);
        Task<BaseResponse<bool>> DeleteGenreAsync(int genreId);
        BaseResponse<IEnumerable<GenreDTO>> GetAllGenres();
        Task<BaseResponse<GenreDTO>> GetGenreByIdAsync(int genreId);
        Task<BaseResponse<GenreDTO>> UpdateGenreAsync(GenreDTO genre, int Id);
    }
}