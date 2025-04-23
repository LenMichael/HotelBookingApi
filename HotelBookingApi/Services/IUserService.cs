using HotelBookingApi.Controllers;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IUserService
    {
        Task Register(RegisterDto registerDto, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
        Task<User?> GetUserById(int id, CancellationToken cancellationToken);
        Task<User> CreateUser(User user, CancellationToken cancellationToken);
        Task<User> UpdateUser(int id, User user, CancellationToken cancellationToken);
        Task<bool> DeleteUser(int id, CancellationToken cancellationToken);
    }
}
