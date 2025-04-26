using HotelBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userService.GetAllUsers(cancellationToken);
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
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.Register(registerDto, cancellationToken);
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
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _authService.Login(loginDto, cancellationToken);
                _logger.LogInformation("User {Username} logged in successfully.", loginDto.Username);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Login failed for user {Username}: {Message}", loginDto.Username, ex.Message);
                return Unauthorized(ex.Message);
            }
        }
    }

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
