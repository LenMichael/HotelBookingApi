using HotelBookingApi.Controllers;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
