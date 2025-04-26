using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(employee => employee.HotelId)
                .GreaterThan(0).WithMessage("Hotel ID must be greater than 0.");

            RuleFor(employee => employee.Position)
                .NotEmpty().WithMessage("Position is required.")
                .Matches("^(Manager|Receptionist|Housekeeping|Chef)$")
                .WithMessage("Position must be 'Manager', 'Receptionist', 'Housekeeping', or 'Chef'.");
        }
    }
}
