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
                                   Result = new List<CheckoutDTO>()
                                   {
                                       new CheckoutDTO {DueDate = DateTime.Today.AddDays(2),CheckOutDate = DateTime.Now,book = "6576b94b850a650d57ab87d5" },
                                       new CheckoutDTO {DueDate = DateTime.Today.AddDays(3),CheckOutDate = DateTime.Now,book = "6576bb5d850a650d57ab87d6" },
                                   }
                               });

            var controller = new CheckoutController(mockCheckoutService.Object);

            // Act
            var result = await controller.Get();            
        }

        [Fact]
        public async Task GetTransactionById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockCheckoutService = new Mock<ICheckoutService>();
            // Setup mock service behavior

            var controller = new CheckoutController(mockCheckoutService.Object);

            // Act
            var result = await controller.GetTransactionById(9); 

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
        }

       
        [Fact]
        public async Task ReturnBook_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var mockCheckoutService = new Mock<ICheckoutService>();
            // Setup mock service behavior

            var controller = new CheckoutController(mockCheckoutService.Object);

            // Act
            var result = await controller.ReturnBook(new ReturnBookDTO()); 
           
        }
    }
}
