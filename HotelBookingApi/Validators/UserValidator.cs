using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters.");

            RuleFor(user => user.PasswordHash)
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(user => user.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Matches("^(Admin|IT|Employee|Guest|Auditor)$")
                .WithMessage("Role must be either 'Admin', 'IT', 'Guest', 'Auditor' or 'Employee'.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.");
        }
    }
}
