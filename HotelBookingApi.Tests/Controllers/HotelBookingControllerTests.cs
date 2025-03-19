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

namespace HotelBookingApi.Tests.Controllers
{
    public class HotelBookingControllerTests
    {
        private readonly Mock<ApiContext> _mockContext;
        private readonly Mock<ILogger<HotelBookingController>> _mockLogger;
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly HotelBookingController _controller;

        public HotelBookingControllerTests()
        {
            _mockContext = new Mock<ApiContext>(new DbContextOptions<ApiContext>());
            _mockLogger = new Mock<ILogger<HotelBookingController>>();
            _controller = new HotelBookingController(_mockContext.Object, _mockLogger.Object , _mockCache.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfBookings()
        {
            // Arrange
            var bookings = new List<HotelBooking>
            {
                new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" },
                new HotelBooking { Id = 2, RoomNumber = 102, ClientName = "Jane Smith" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<HotelBooking>>();
            mockSet.As<IQueryable<HotelBooking>>().Setup(m => m.Provider).Returns(bookings.Provider);
            mockSet.As<IQueryable<HotelBooking>>().Setup(m => m.Expression).Returns(bookings.Expression);
            mockSet.As<IQueryable<HotelBooking>>().Setup(m => m.ElementType).Returns(bookings.ElementType);
            mockSet.As<IQueryable<HotelBooking>>().Setup(m => m.GetEnumerator()).Returns(bookings.GetEnumerator());

            _mockContext.Setup(c => c.GetBookings()).Returns(mockSet.Object);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBookings = Assert.IsType<List<HotelBooking>>(okResult.Value);
            Assert.Equal(2, returnBookings.Count);
        }
    }
}
