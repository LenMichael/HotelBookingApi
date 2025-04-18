﻿using HotelBookingApi.Controllers;
using HotelBookingApi.Models;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HotelBookingApi.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IAuthenticationService> _mockAuthService;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_mockUserService.Object, _mockAuthService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithUsers()
        {
            // Arrange
            var users = new List<User>
                {
                    new() { Id = 1, Username = "admin", PasswordHash = "hashedpassword1", Role = "Admin" },
                    new() { Id = 2, Username = "user", PasswordHash = "hashedpassword2", Role = "User" }
                };
            _mockUserService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count());
        }

        [Fact]
        public async Task GetAllUsers_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetAllUsersAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while retrieving users.", statusCodeResult.Value);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var registerDto = new RegisterDto { Username = "newuser", Password = "password123" };
            _mockUserService.Setup(s => s.RegisterAsync(registerDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully.", okResult.Value);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenUsernameAlreadyExists()
        {
            // Arrange
            var registerDto = new RegisterDto { Username = "existinguser", Password = "password123" };
            _mockUserService.Setup(s => s.RegisterAsync(registerDto)).ThrowsAsync(new InvalidOperationException("Username already exists."));

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username already exists.", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsOk_WithToken()
        {
            // Arrange
            var loginDto = new LoginDto { Username = "user", Password = "password123" };
            var token = "mocked-jwt-token";
            _mockAuthService.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(token);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedToken = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal(token, returnedToken["Token"]);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDto { Username = "user", Password = "wrongpassword" };
            _mockAuthService.Setup(s => s.LoginAsync(loginDto)).ThrowsAsync(new UnauthorizedAccessException("Invalid username or password."));

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password.", unauthorizedResult.Value);
        }
    }
}
