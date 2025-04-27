using HotelBookingApi.Controllers;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (await _userRepository.UserExists(registerDto.Username, cancellationToken))
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User",
                Email = registerDto.Email
            };

            await _userRepository.Add(user, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAll(cancellationToken);
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found.");
            return user;
        }

        public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
        {
            if (await _userRepository.UserExists(user.Username, cancellationToken))
                throw new InvalidOperationException("Username already exists.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            return await _userRepository.Add(user, cancellationToken);
        }

        public async Task<User> UpdateUser(int id, User user, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetById(id, cancellationToken);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            existingUser.Username = user.Username;
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            existingUser.Role = user.Role;

            return await _userRepository.Add(existingUser, cancellationToken);
        }

        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            return await _userRepository.Delete(id, cancellationToken);
        }
    }
}
