using HotelBookingApi.Controllers;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }

}
