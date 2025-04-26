using FluentValidation;
using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HotelBookingApi.IntegrationTests.Services
{
    public class HotelBookingServiceTests
    {
        private readonly ApiContext _context;
        private readonly HotelBookingService _service;

        public HotelBookingServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApiContext(options);

            var mockRepository = new Mock<IHotelBookingRepository>();
            var mockValidator = new Mock<IValidator<Booking>>();
            _service = new HotelBookingService(/*_context*/mockRepository.Object, mockValidator.Object);
        }

        [Fact]
        public async Task CreateBooking_Should_Add_Booking_To_Database()
        {
            // Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var booking = new Booking
            {
                UserId = 1,
                RoomId = 101,
                CheckInDate = DateTime.UtcNow,
                CheckOutDate = DateTime.UtcNow.AddDays(2)
            };

            // Act
            await _service.CreateBooking(booking, cancellationToken);
            var savedBooking = await _context.Bookings.FindAsync(booking.Id);

            // Assert
            Assert.NotNull(savedBooking);
            Assert.Equal(booking.UserId, savedBooking.UserId);
        }

        [Fact]
        public async Task GetBookingById_Should_Return_Correct_Booking()
        {
            // Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var booking = new Booking
            {
                UserId = 1,
                RoomId = 101,
                CheckInDate = DateTime.UtcNow,
                CheckOutDate = DateTime.UtcNow.AddDays(2)
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetBookingById(booking.Id, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(booking.Id, result.Id);
        }

        [Fact]
        public async Task CancelBooking_Should_Update_Booking_Status()
        {
            // Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            var booking = new Booking
            {
                UserId = 1,
                RoomId = 101,
                CheckInDate = DateTime.UtcNow,
                CheckOutDate = DateTime.UtcNow.AddDays(2),
                Status = "Cancelled"
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Act
            await _service.UpdateBooking(booking, cancellationToken);
            var updatedBooking = await _context.Bookings.FindAsync(booking.Id);

            // Assert
            Assert.NotNull(updatedBooking);
            Assert.StrictEqual(updatedBooking.Status, "Cancelled");
        }

        [Fact]
        public async Task GetAllBookings_Should_Return_All_Bookings()
        {
            // Arrange
            var cancellationToken = new CancellationTokenSource().Token;
            _context.Bookings.Add(new Booking { UserId = 1, RoomId = 101 });
            _context.Bookings.Add(new Booking { UserId = 2, RoomId = 102 });
            await _context.SaveChangesAsync();

            // Act
            var bookings = await _service.GetAllBookings(cancellationToken);

            // Assert
            Assert.Equal(2, bookings.Count());
        }
    }
}
