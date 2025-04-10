using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}

