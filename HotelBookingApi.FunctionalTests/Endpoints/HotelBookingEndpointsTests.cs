//using System.Net;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using HotelBookingApi.Models;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using Xunit;

//namespace HotelBookingApi.FunctionalTests.Endpoints
//{
//    public class HotelBookingEndpointsTests : IClassFixture<CustomWebApplicationFactory<Program>>
//    {
//        private readonly HttpClient _client;

//        public HotelBookingEndpointsTests(CustomWebApplicationFactory<Program> factory)
//        {
//            _client = factory.CreateClient();
//        }

//        [Fact]
//        public async Task Post_HotelBooking_Should_Return_Created_Status()
//        {
//            // Arrange
//            var newBooking = new Booking
//            {
//                UserId = 1,
//                RoomId = 101,
//                CheckInDate = DateTime.UtcNow,
//                CheckOutDate = DateTime.UtcNow.AddDays(2)
//            };

//            // Act
//            var response = await _client.PostAsJsonAsync("/api/hotelbooking", newBooking);

//            // Assert
//            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

//            var createdBooking = await response.Content.ReadFromJsonAsync<Booking>();
//            Assert.NotNull(createdBooking);
//            Assert.Equal(newBooking.UserId, createdBooking.UserId);
//        }
//    }

//}
