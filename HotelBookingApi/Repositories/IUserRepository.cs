using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
        Task<User?> GetById(int id, CancellationToken cancellationToken);
        Task<User> Add(User user, CancellationToken cancellationToken);
        Task<User> Update(User user, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
