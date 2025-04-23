using HotelBookingApi.Controllers;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken);
        string GenerateJwtToken(User user);
    }

}
