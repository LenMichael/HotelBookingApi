using FluentValidation.TestHelper;
using HotelBookingApi.Models;
using HotelBookingApi.Validators;
using Xunit;

namespace HotelBookingApi.Tests.Validators
{
    public class UserValidatorTests
    {
        private readonly UserValidator _validator;

        public UserValidatorTests()
        {
            _validator = new UserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Username_Is_Empty()
        {
            var user = new User { Username = "", PasswordHash = "hash", Role = "Admin" };
            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.Username)
                .WithErrorMessage("Username is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Username_Exceeds_Max_Length()
        {
            var user = new User { Username = new string('a', 101), PasswordHash = "hash", Role = "Admin" };
            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.Username)
                .WithErrorMessage("Username cannot exceed 100 characters.");
        }

        [Fact]
        public void Should_Have_Error_When_PasswordHash_Is_Empty()
        {
            var user = new User { Username = "ValidUser", PasswordHash = "", Role = "Admin" };
            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.PasswordHash)
                .WithErrorMessage("Password is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Role_Is_Invalid()
        {
            var user = new User { Username = "ValidUser", PasswordHash = "hash", Role = "InvalidRole" };
            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.Role)
                .WithErrorMessage("Role must be either 'Admin', 'IT', 'Guest', 'Auditor' or 'Employee'.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_User_Is_Valid()
        {
            var user = new User { Username = "ValidUser", PasswordHash = "hash", Role = "Admin" };
            var result = _validator.TestValidate(user);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
