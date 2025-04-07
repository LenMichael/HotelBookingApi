using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IMemoryCache _cache;

        public AuthController(ApiContext context, IConfiguration configuration, ILogger<AuthController> logger, IMemoryCache cache)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _cache = cache;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }


        //public IActionResult GetUserInfo()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return Unauthorized("User not found.");
        //    }
        //    var user = _context.Users.Find(userId);
        //    if (user == null)
        //    {
        //        return NotFound("User not found.");
        //    }
        //    return Ok(new { user.Username, user.Role });
        //}


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (_cache.TryGetValue(registerDto.Username, out User cachedUser) || await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                return BadRequest("Username already exists.");
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

            return Ok("User registered successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!_cache.TryGetValue(loginDto.Username, out User user))
            {
                user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
                if (user != null)
                {
                    _cache.Set(user.Username, user);
                }
            }

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the JWT token.");
                throw;
            }
        }
    }



    //    [HttpPost]
    //    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    //    {
    //        if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
    //        {
    //            return BadRequest("Username already exists.");
    //        }

    //        var user = new User
    //        {
    //            Username = registerDto.Username,
    //            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
    //        };

    //        _context.Users.Add(user);
    //        await _context.SaveChangesAsync();

    //        return Ok("User registered successfully.");
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    //    {
    //        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
    //        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
    //        {
    //            return Unauthorized("Invalid username or password.");
    //        }

    //        //return Ok("User Logged in");
    //        var token = GenerateJwtToken(user);
    //        return Ok(new { Token = token });
    //    }

    //    private string GenerateJwtToken(User user)
    //    {
    //        try
    //        {
    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
    //            var tokenDescriptor = new SecurityTokenDescriptor
    //            {
    //                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
    //                Expires = DateTime.UtcNow.AddDays(7),
    //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //            };
    //            var token = tokenHandler.CreateToken(tokenDescriptor);
    //            return tokenHandler.WriteToken(token);

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "An error occurred while generating the JWT token.");
    //            throw;
    //        }
    //    }
    //}

    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
