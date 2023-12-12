using LMIS.API.Controllers.Books;
using LMIS.Api.Services.Services.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Core.DTOs.Book;
using Microsoft.AspNetCore.Mvc;
using LMIS.Api.Core.Model;

namespace LMIS.Test
{
    public class BookControllerTest
    {
        private readonly Mock<IBookService> _mockBookService;
        private readonly BooksController _controller;

        public BookControllerTest()
        {
            _mockBookService = new Mock<IBookService>();
            _controller = new BooksController(_mockBookService.Object);
        }
        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            _mockBookService.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());
            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_WhenExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            _mockBookService.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new NullReferenceException("Null reference encountered"));

            // Act
            var result = await _controller.Get();

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }     
       



        [Fact]
        public async Task CreateBook_WhenModelStateInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await _controller.CreateBook(new CreateBookDTO());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}
