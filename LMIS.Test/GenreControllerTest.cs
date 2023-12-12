using LMIS.API.Controllers.Genre;
using LMIS.Api.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Services.Services.IServices;
using LMIS.Api.Core.DTOs.Genre;

namespace LMIS.Test
{
    public class GenreControllerTest
    {
        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            // Arrange
            var mockGenreService = new Mock<IGenreService>();
            mockGenreService.Setup(service => service.GetAllGenres())
                            .Returns(new BaseResponse<IEnumerable<GenreDTO>> { IsError = false, Result = new List<GenreDTO>() });

            var controller = new GenreController(null, mockGenreService.Object);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task GetGenreById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int validGenreId = 1;
            var mockGenreService = new Mock<IGenreService>();
            mockGenreService.Setup(service => service.GetGenreByIdAsync(validGenreId))
                            .ReturnsAsync(new BaseResponse<GenreDTO> { IsError = false, Result = new GenreDTO() });

            var controller = new GenreController(null, mockGenreService.Object);

            // Act
            var result = await controller.GetGenreById(validGenreId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task CreateGenre_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var mockGenreService = new Mock<IGenreService>();
            mockGenreService.Setup(service => service.CreateGenre(It.IsAny<GenreDTO>(), It.IsAny<string>()))
                            .ReturnsAsync(new BaseResponse<GenreDTO> { IsError = false, Result = new GenreDTO() });

            var controller = new GenreController(null, mockGenreService.Object);
            var genre = new GenreDTO { Name = "Fantasy", MaximumBooksAllowed = 10 };

            // Act
            var result = await controller.CreateGenre(genre);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }
        [Fact]
        public async Task UpdateGenre_WithValidData_ReturnsOkResult()
        {
            // Arrange
            int validGenreId = 1;
            var mockGenreService = new Mock<IGenreService>();
            mockGenreService.Setup(service => service.UpdateGenreAsync(It.IsAny<GenreDTO>(), validGenreId))
                            .ReturnsAsync(new BaseResponse<GenreDTO> { IsError = false, Result = new GenreDTO() });

            var controller = new GenreController(null, mockGenreService.Object);
            var genre = new GenreDTO { Name = "Science Fiction", MaximumBooksAllowed = 15 };

            // Act
            var result = await controller.UpdateGenre(validGenreId, genre);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int validGenreId = 1;
            var mockGenreService = new Mock<IGenreService>();
            mockGenreService.Setup(service => service.DeleteGenreAsync(validGenreId))
                            .ReturnsAsync(new BaseResponse<bool> { IsError = false, Result = true });

            var controller = new GenreController(null, mockGenreService.Object);
            // Act
            var result = await controller.Delete(validGenreId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }


    }
}
