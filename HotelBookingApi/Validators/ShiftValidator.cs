using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class ShiftValidator : AbstractValidator<Shift>
    {
        public ShiftValidator()
        {
            RuleFor(shift => shift.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

            RuleFor(shift => shift.StartTime)
                .NotEmpty().WithMessage("Start time is required.");

            RuleFor(shift => shift.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .GreaterThan(shift => shift.StartTime)
                .WithMessage("End time must be after the start time.");

            RuleFor(shift => shift.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(50).WithMessage("Role cannot be longer than 50 characters.");
        }
    }
}
