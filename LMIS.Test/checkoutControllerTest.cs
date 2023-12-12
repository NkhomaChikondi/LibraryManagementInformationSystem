using LMIS.API.Controllers.CheckoutController;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Services.Services.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using LMIS.Api.Core.Model;
using Org.BouncyCastle.Crypto.Macs;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LMIS.Test
{
    public class checkoutControllerTest
    {
        [Fact]
        public async Task GetAllTransactions_ReturnsOkResult()
        {
            // Arrange
            var mockCheckoutService = new Mock<ICheckoutService>();
            mockCheckoutService.Setup(service => service.GetAllCheckoutTransactions())
                               .ReturnsAsync(new BaseResponse<IEnumerable<CheckoutDTO>>
                               {
                                   IsError = false,
                                   Result = new List<CheckoutDTO>
                                   {
                               new CheckoutDTO { CheckOutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(2), Book = "TITLE" }
                                   }
                               });

            var controller = new CheckoutController(mockCheckoutService.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var transactions = Assert.IsAssignableFrom<IEnumerable<CheckoutDTO>>(okResult.Value);
            Assert.NotNull(transactions);
        }

        [Fact]
        public async Task GetTransactionById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockCheckoutService = new Mock<ICheckoutService>();
            // Setup mock service behavior
            mockCheckoutService.Setup(service => service.GetCheckoutTransactionByIdAsync(9))
               .ReturnsAsync(new BaseResponse<CheckoutDTO>());
            var controller = new CheckoutController(mockCheckoutService.Object);

            // Act
            var result = await controller.GetTransactionById(9);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var transaction = Assert.IsType<BaseResponse<CheckoutDTO>>(okResult.Value);
            Assert.NotNull(transaction);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockCheckoutService = new Mock<ICheckoutService>();
            // Setup mock service behavior

            var controller = new CheckoutController(mockCheckoutService.Object);

            // Act
            var result = await controller.Delete(1);           
           
            Assert.NotNull(result);
        }

       
        [Fact]
        public async Task ReturnBook_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var mockCheckoutService = new Mock<ICheckoutService>();
            mockCheckoutService.Setup(service => service.ReturnBook(It.IsAny<ReturnBookDTO>(), It.IsAny<string>()))
                               .ReturnsAsync(new BaseResponse<bool>
                               {
                                   IsError = false,
                                   Result = true,
                                   Message = "Success"
                               });

            var controller = new CheckoutController(mockCheckoutService.Object);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "Chikondinkhoma51@gmail.com")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Set the User property on the ControllerContext
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var ReturnRequest = new ReturnBookDTO
            {
                ISBN = "khjdsdh",
                MemberCode = "hN0001"
            };

            // Act
            var result = await controller.ReturnBook(ReturnRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<bool>(okResult.Value);
            Assert.NotNull(response);             
        }

    }
}
