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
        public async Task GetAll_ReturnsOkResult_WithListOfBookings()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new Booking { Id = 1, RoomId = 101, UserId = 1, CheckInDate = DateTime.UtcNow, CheckOutDate = DateTime.UtcNow.AddDays(2), Status = "Confirmed" },
                new Booking { Id = 2, RoomId = 102, UserId = 2, CheckInDate = DateTime.UtcNow, CheckOutDate = DateTime.UtcNow.AddDays(3), Status = "Cancelled" }
            };
            _mockService.Setup(s => s.GetAllBookings(It.IsAny<CancellationToken>())).ReturnsAsync(bookings);

            // Act
            var result = await _controller.GetAll(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBookings = Assert.IsType<List<Booking>>(okResult.Value);
            Assert.Equal(2, returnBookings.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenBookingExists()
        {
            // Arrange
            var booking = new Booking { Id = 1, RoomId = 101, UserId = 1, CheckInDate = DateTime.UtcNow, CheckOutDate = DateTime.UtcNow.AddDays(2), Status = "Confirmed" };
            _mockService.Setup(s => s.GetBookingById(1, It.IsAny<CancellationToken>())).ReturnsAsync(booking);

            // Act
            var result = await _controller.GetById(1, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetBookingById(1, It.IsAny<CancellationToken>())).ReturnsAsync((Booking)null);

            // Act
            var result = await _controller.GetById(1, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenBookingIsValid()
        {
            // Arrange
            var booking = new Booking { Id = 1, RoomId = 101, UserId = 1, CheckInDate = DateTime.UtcNow, CheckOutDate = DateTime.UtcNow.AddDays(2), Status = "Confirmed" };
            _mockService.Setup(s => s.CreateBooking(booking, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(booking, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public async Task Edit_ReturnsOkResult_WhenBookingIsValid()
        {
            // Arrange
            var booking = new Booking { Id = 1, RoomId = 101, UserId = 1, CheckInDate = DateTime.UtcNow, CheckOutDate = DateTime.UtcNow.AddDays(2), Status = "Confirmed" };
            _mockService.Setup(s => s.UpdateBooking(booking, It.IsAny<CancellationToken>())).ReturnsAsync(booking);

            // Act
            var result = await _controller.Edit(1, booking, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            var booking = new Booking { Id = 1, RoomId = 101, UserId = 1, CheckInDate = DateTime.UtcNow, CheckOutDate = DateTime.UtcNow.AddDays(2), Status = "Confirmed" };
            _mockService.Setup(s => s.UpdateBooking(booking, It.IsAny<CancellationToken>())).ReturnsAsync((Booking)null);

            // Act
            var result = await _controller.Edit(1, booking, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenBookingExists()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteBooking(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1, CancellationToken.None);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteBooking(1, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(1, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
