using AutoMapper;
using LMIS.API.Controllers.User;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

public class AuthControllerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _configurationMock = new Mock<IConfiguration>();

        _authController = new AuthController(_unitOfWorkMock.Object, _mapperMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Login_ReturnsOkObjectResult_WhenCredentialsAreCorrect()
    {
        // Arrange
        var user = new ApplicationUser 
        {
            Email = "chikondinkhoma51@gmail.com", 
            Password = "cnaQ7A^" 
                                         
        };
        var loginModel = new LoginDTO
        {
            Email = "chikondinkhoma51@gmail.com",
            Password = "cnaQ7A^"
        };

        _unitOfWorkMock.Setup(u => u.User.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                       .ReturnsAsync(user); 

        _unitOfWorkMock.Setup(u => u.User.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(true);
        // Act
        var result = await _authController.Login(loginModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenCredentialsAreIncorrect()
    {
        // Arrange
        var loginModel = new LoginDTO
        {
            Email = "chikondinkhoma51@gmail.com",
            Password = "cnaQ7"
        };

        _unitOfWorkMock.Setup(u => u.User.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                       .ReturnsAsync((ApplicationUser)null); // Simulate user not found in the database

        // Act
        var result = await _authController.Login(loginModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Login Credentials do not match", badRequestResult.Value);
    }

}
