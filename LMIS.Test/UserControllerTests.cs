using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Services.Services.IServices;
using LMIS.API.Controllers.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LMIS.Test
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

        public UserControllerTests()
        {
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]        
        public async Task GetUserById_ReturnsBadRequest_WhenUserServiceReturnsError()
        {
            // Arrange
            int userId = 1;
            var errorResponse = new BaseResponse<ApplicationUserDTO>()
            {
                IsError = true,
                Message = "No user found or operation failed"
            };

            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                            .ReturnsAsync(errorResponse);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No user found or operation failed", badRequestResult.Value);
        }


        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");
            var createUserDTO = new ApplicationUserDTO(); 

            // Act
            var result = await _controller.CreateUser(createUserDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }        

        [Fact]
        public void ResendPin_ReturnsCorrectResponse_WhenResendEmailCalled()
        {
            // Arrange
            string userEmail = "chikondinkhoma51@gmail.com";

            // Case 1: Simulate null email
           _userServiceMock.Setup(service => service.ResendEmail(null))
                            .ReturnsAsync(new BaseResponse<bool>
                            {
                                IsError = true,
                                Message = " Please enter your email"
                            });

            // Case 2: Simulate user not found
            _userServiceMock.Setup(service => service.ResendEmail(userEmail))
                            .ReturnsAsync(new BaseResponse<bool>
                            {
                                IsError = true,
                                Message = " Failed to find user with the provided email"
                            });

            // Case 3: Simulate successful email resend
            _userServiceMock.Setup(service => service.ResendEmail(userEmail))
                            .ReturnsAsync(new BaseResponse<bool>
                            {
                                IsError = false,
                                Message = " Email is resent successfully"
                            });

           
        }

    }
}