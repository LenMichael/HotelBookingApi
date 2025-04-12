using HotelBookingApi.Controllers;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (await _userRepository.UserExistsAsync(registerDto.Username))
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User"
            };

            await _userRepository.AddUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }

}
