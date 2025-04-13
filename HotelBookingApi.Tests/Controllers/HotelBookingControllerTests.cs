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
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    RoomId = 1,
                    UserId = 2,
                    CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                    CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                    Status = "Confirmed"
                },
                new Booking
                {
                    Id = 2,
                    RoomId = 2,
                    UserId = 2,
                    CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 5, 1), DateTimeKind.Utc),
                    CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 5, 5), DateTimeKind.Utc),
                    Status = "Cancelled"
                }
            };
            _mockService.Setup(s => s.GetAllBookings()).Returns(bookings);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBookings = Assert.IsType<List<Booking>>(okResult.Value);
            Assert.Equal(2, returnBookings.Count);
        }

        [Fact]
        public void Get_ReturnsOkResult_WhenBookingExists()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 1,
                RoomId = 1,
                UserId = 2,
                CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                Status = "Confirmed"
            };
            _mockService.Setup(s => s.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetBookingById(1)).Returns((Booking)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsOkResult_WhenBookingIsValid()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 1,
                RoomId = 1,
                UserId = 2,
                CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                Status = "Confirmed"
            };

            // Act
            var result = _controller.Create(booking);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal(booking.Room.RoomNumber, returnBooking.Room.RoomNumber);
        }

        [Fact]
        public void Edit_ReturnsOkResult_WhenBookingIsValid()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 1,
                RoomId = 1,
                UserId = 2,
                CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                Status = "Confirmed"
            };
            _mockService.Setup(s => s.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.Edit(1, booking);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooking = Assert.IsType<Booking>(okResult.Value);
            Assert.Equal(1, returnBooking.Id);
        }

        [Fact]
        public void Edit_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 1,
                RoomId = 1,
                UserId = 2,
                CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                Status = "Confirmed"
            };
            _mockService.Setup(s => s.GetBookingById(1)).Returns((Booking)null);

            // Act
            var result = _controller.Edit(1, booking);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenBookingExists()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 1,
                RoomId = 1,
                UserId = 2,
                CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                Status = "Confirmed"
            };
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
            _mockService.Setup(s => s.GetBookingById(1)).Returns((Booking)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
