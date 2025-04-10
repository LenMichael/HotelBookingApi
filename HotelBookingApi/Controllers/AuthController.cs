using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Services;
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
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, IAuthenticationService authService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _authService = authService;
            _logger = logger;
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                _logger.LogInformation("Admin retrieved all users.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "An error occurred while retrieving users.");
            }
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
            try
            {
                await _userService.RegisterAsync(registerDto);
                _logger.LogInformation("User {Username} registered successfully.", registerDto.Username);
                return Ok("User registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Registration failed for user {Username}: {Message}", registerDto.Username, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDto);
                _logger.LogInformation("User {Username} logged in successfully.", loginDto.Username);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Login failed for user {Username}: {Message}", loginDto.Username, ex.Message);
                return Unauthorized(ex.Message);
            }
        }

        //private string GenerateJwtToken(User user)
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //                new Claim(ClaimTypes.Role, user.Role)
        //            }),
        //            //Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //            Expires = DateTime.UtcNow.AddDays(7),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return tokenHandler.WriteToken(token);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while generating the JWT token.");
        //        throw;
        //    }
        //}
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
