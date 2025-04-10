using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using HotelBookingApi.Controllers;
using HotelBookingApi.Data;
using HotelBookingApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using HotelBookingApi.Services;

namespace HotelBookingApi.Tests.Controllers
{
    public class HotelBookingControllerTests
    {
        private readonly Mock<IHotelBookingService> _mockService;
        private readonly Mock<ILogger<HotelBookingController>> _mockLogger;
        private readonly HotelBookingController _controller;

        public HotelBookingControllerTests()
        {
            _mockService = new Mock<IHotelBookingService>();
            _mockLogger = new Mock<ILogger<HotelBookingController>>();
            _controller = new HotelBookingController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfBookings()
        {
            // Arrange
            var bookings = new List<HotelBooking>
            {
                new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" },
                new HotelBooking { Id = 2, RoomNumber = 102, ClientName = "Jane Smith" }
            };
            _mockService.Setup(s => s.GetAllBookings()).Returns(bookings);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBookings = Assert.IsType<List<HotelBooking>>(okResult.Value);
            Assert.Equal(2, returnBookings.Count);
        }

        [Fact]
        public void Get_ReturnsOkResult_WhenBookingExists()
        {
            // Arrange
            var booking = new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" };
            _mockService.Setup(s => s.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<HotelBooking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetBookingById(1)).Returns((HotelBooking)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsOkResult_WhenBookingIsValid()
        {
            // Arrange
            var booking = new HotelBooking { RoomNumber = 101, ClientName = "John Doe" };

            // Act
            var result = _controller.Create(booking);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<HotelBooking>(okResult.Value);
            Assert.Equal(booking.RoomNumber, returnBooking.RoomNumber);
        }

        [Fact]
        public void Edit_ReturnsOkResult_WhenBookingIsValid()
        {
            // Arrange
            var booking = new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" };
            _mockService.Setup(s => s.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.Edit(1, booking);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<HotelBooking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public void Edit_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            var booking = new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" };
            _mockService.Setup(s => s.GetBookingById(1)).Returns((HotelBooking)null);

            // Act
            var result = _controller.Edit(1, booking);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenBookingExists()
        {
            // Arrange
            var booking = new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" };
            _mockService.Setup(s => s.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetBookingById(1)).Returns((HotelBooking)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
