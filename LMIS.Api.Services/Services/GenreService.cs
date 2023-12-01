using AutoMapper;
using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IBookService _bookService;

        public GenreService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService, IBookService bookService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _bookService = bookService;
        }

        public async Task<GenreDTO> CreateGenre(GenreDTO genre, string userIdClaim)
        {
            try
            {
                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == userEmail);
                if (user == null)
                {
                    return null;
                }
                // create a new genre
                var newGenre = new Genre
                {
                    Name = genre.Name,
                    MaximumBooksAllowed = genre.MaximumBooksAllowed,
                    user = user,
                    userId = user.UserId
                };
                await _unitOfWork.Genre.CreateAsync(newGenre);
                _unitOfWork.Save();

                var newGenreDto = new GenreDTO
                {
                    Name = genre.Name,
                    MaximumBooksAllowed = genre.MaximumBooksAllowed
                };

                return newGenreDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<GenreDTO> GetAllGenres()
        {
            try
            {
                var allGenres = _unitOfWork.Genre.GetAllAsync();
                var allgenreDTO = _mapper.Map<IEnumerable<GenreDTO>>(allGenres);

                return allgenreDTO;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        public async Task<GenreDTO> GetGenreByIdAsync(int genreId)
        {
            try
            {
                var genre = await _unitOfWork.Genre.GetByIdAsync(genreId);

                var getGenreDTO = _mapper.Map<GenreDTO>(genre);
                return getGenreDTO;              
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<GenreDTO> UpdateGenreAsync(GenreDTO genre, int Id)
        {
            try
            {

                var selectedGenre = await _unitOfWork.Genre.GetByIdAsync(Id);
                selectedGenre.Name = genre.Name;
                selectedGenre.MaximumBooksAllowed = genre.MaximumBooksAllowed;
                _unitOfWork.Genre.Update(selectedGenre);
                _unitOfWork.Save();
                try
                {
                    var books = await _bookService.GetAllAsync();
                    if (books != null)
                    {
                        // get all books having the genre's name
                        var selectGenres = books.Where(b => b.Genre == selectedGenre.Name).ToList();
                        if (selectGenres.Count > 0)
                        {
                            foreach (var book in selectGenres)
                            {
                                book.Genre = selectedGenre.Name;
                                await _bookService.UpdateAsync(book.Id, book);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
               
                var getGenreDTO = _mapper.Map<GenreDTO>(selectedGenre);
                return getGenreDTO;               
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            try
            {
                var genre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(g => g.GenreId == genreId);
                if (genre != null)
                {
                    try
                    {
                        var books = await _bookService.GetAllAsync();
                        if (books != null)
                        {
                            // get all books having the genre's name
                            var selectGenres = books.Where(b => b.Genre == genre.Name).ToList();
                            if (selectGenres.Count > 0)
                                return;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                   
                    await _unitOfWork.Genre.DeleteAsync(genreId);
                    _unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
