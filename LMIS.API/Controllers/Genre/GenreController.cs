using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.Genre
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IConfiguration configuration, IGenreService genreService)
        {
            _genreService = genreService;
        }
        // GET: api/<GenreController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {
            try
            {
                var response = _genreService.GetAllGenres();
                if (response == null)
                {
                    return BadRequest("Failed to get all genre");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting Genres");
            }
        }

        // GET api/<GenreController>/5
        [HttpGet("GetGenreById/{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var response = await _genreService.GetGenreByIdAsync(id);
                if (response == null)
                {
                    return BadRequest("Failed to get genre");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST api/<GenreController>
        [HttpPost("CreateGenre")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized("You are not authorized to create member");
                }

                var response = await _genreService.CreateGenre(genre, userIdClaim);

                if (response == null)
                {
                    return BadRequest("Failed to create genre");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating a genre");
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("Update/{genreId}")]
        public async Task<IActionResult> UpdateGenre(int genreId, [FromBody] GenreDTO Genre)
        {
            try
            {
                var response = await _genreService.UpdateGenreAsync(Genre, genreId);
                if (response == null)
                    return BadRequest("failed to update genre");
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while updating the genre.");
            }

        }

        // DELETE api/<GenreController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _genreService.DeleteGenreAsync(id);                 
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while updating the genre.");
            }

        }
    }
}
