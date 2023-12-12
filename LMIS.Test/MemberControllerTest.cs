using LMIS.API.Controllers.Member;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Services.Services.IServices;

namespace LMIS.Test
{
    public class MemberControllerTest
    {
        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            // Arrange
            var mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(service => service.GetAllMembers())
                             .ReturnsAsync(new BaseResponse<IEnumerable<CreateMemberDto>> { IsError = false, Result = new List<CreateMemberDto>() });

            var controller = new MemberController(null, mockMemberService.Object);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task GetMemberById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int validMemberId = 1;
            var mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(service => service.GetMemberByIdAsync(validMemberId))
                             .ReturnsAsync(new BaseResponse<CreateMemberDto> { IsError = false, Result = new CreateMemberDto() });

            var controller = new MemberController(null, mockMemberService.Object);

            // Act
            var result = await controller.GetMemberById(validMemberId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task CreateMember_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(service => service.CreateMemberAsync(It.IsAny<CreateMemberDto>(), It.IsAny<string>()))
                             .ReturnsAsync(new BaseResponse<CreateMemberDto> { IsError = false, Result = new CreateMemberDto() });

            var controller = new MemberController(null, mockMemberService.Object);
            var member = new CreateMemberDto {
            
               Email = "Chikondinkhoma51@gmail.com",
               FirstName = "Test",
               LastName = "Test",
               MemberTypeName = "Test",
               Phone = "0987654323"
            };

            // Act
            var result = await controller.CreateMember(member);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task UpdateMember_WithValidData_ReturnsOkResult()
        {
            // Arrange
            int validMemberId = 1;
            var mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(service => service.UpdateMemberAsync(It.IsAny<CreateMemberDto>(), validMemberId))
                             .ReturnsAsync(new BaseResponse<bool> { IsError = false, Result = true });

            var controller = new MemberController(null, mockMemberService.Object);
            var memberDto = new CreateMemberDto { /* Add necessary properties for member DTO */ };

            // Act
            var result = await controller.UpdateMember(validMemberId, memberDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int validMemberId = 1;
            var mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(service => service.DeleteMemberAsync(validMemberId))
                             .ReturnsAsync(new BaseResponse<bool> { IsError = false, Result = true });

            var controller = new MemberController(null, mockMemberService.Object);

            // Act
            var result = await controller.Delete(validMemberId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }
    }
}

