using HotelBookingApi.Controllers;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        string GenerateJwtToken(User user);
    }

}
