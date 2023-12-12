using AutoMapper;
using LMIS.Api.Core.DTOs;
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

        public async Task<BaseResponse<GenreDTO>> CreateGenre(GenreDTO genre, string userIdClaim)
        {
            try
            {
                if (genre == null || string.IsNullOrEmpty(userIdClaim))
                    return new()
                    {
                        IsError = false,
                        Message = "Make sure all values are entered correctly "
                    };

                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to create the user"
                    };

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

                return new()
                {
                    IsError = false,
                    Result = newGenreDto
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = " An error occured, failed to create a user"
                };
            }
        }



        public BaseResponse<IEnumerable<GenreDTO>> GetAllGenres()
        {
            try
            {
                var allGenres = _unitOfWork.Genre.GetAllAsync();
                if (allGenres != null)
                {
                    var allGenreDTO = _mapper.Map<IEnumerable<GenreDTO>>(allGenres);
                    return new()
                    {
                        IsError = false,
                        Result = allGenreDTO
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = "Failed to get all genres "
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to get all genres "
                };
            }
        }

        public async Task<BaseResponse<GenreDTO>> UpdateGenreAsync(GenreDTO genre, int Id)
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
                        var selectGenres = books.Where(b => b?.Genre == selectedGenre.Name).ToList();

                        if (selectGenres.Count > 0)
                        {
                            foreach (var book in selectGenres)
                            {
                                if (book != null && book.Id != null)
                                {
                                    // Use book.Id here
                                    await _bookService.UpdateAsync(book.Id, book);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    return new()
                    {
                        IsError = false,
                        Message = "Failed to update Genre"
                    };
                }

                var getGenreDTO = _mapper.Map<GenreDTO>(selectedGenre);
                return new()
                {
                    IsError = false,
                    Result = getGenreDTO,
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to update a genre",
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteGenreAsync(int genreId)
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
                            var selectGenres = books.Where(b => genre != null && b?.Genre == genre.Name).ToList();

                            if (selectGenres.Count > 0)
                                return new()
                                {
                                    IsError = true,
                                    Message = " This genre cannot be deleted, it is connected to other books"
                                };
                        }
                    }
                    catch (Exception)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "Failed to delete this book"
                        };
                    }

                    await _unitOfWork.Genre.DeleteAsync(genreId);
                    _unitOfWork.Save();
                    return new()
                    {
                        IsError = false,
                        Message = "Deleted successfully"
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = "Failed to delete genre"
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = false,
                    Message = "Failed to delete genre"
                };
            }
        }

        public async Task<BaseResponse<GenreDTO>> GetGenreByIdAsync(int genreId)
        {
            try
            {
                var genre = await _unitOfWork.Genre.GetByIdAsync(genreId);
                if (genre != null)
                {
                    var getGenreDTO = _mapper.Map<GenreDTO>(genre);
                    return new()
                    {
                        IsError = false,
                        Result = getGenreDTO
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = "Failed to selected genre"
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to selected genre"
                };
            }
        }
    }
}
