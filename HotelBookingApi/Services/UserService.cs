using HotelBookingApi.Controllers;
using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelBookingApi.Services
{
    public class UserService : IUserService
    {
        private readonly ApiContext _context;
        private readonly IMemoryCache _cache;

        public UserService(ApiContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (_cache.TryGetValue(registerDto.Username, out User? cachedUser) || await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _cache.Set(user.Username, user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }

}
