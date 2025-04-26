using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class MaintenanceRequestValidator : AbstractValidator<MaintenanceRequest>
    {
        public MaintenanceRequestValidator()
        {
            RuleFor(request => request.RoomId)
                .GreaterThan(0).WithMessage("Room ID must be greater than 0.")
                .When(request => request.RoomId.HasValue);

            RuleFor(request => request.Description)
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(request => request.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(50).WithMessage("Status cannot be longer than 50 characters.");

            RuleFor(request => request.CreatedAt)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(request => request.UpdatedAt)
                .GreaterThan(request => request.CreatedAt)
                .WithMessage("Updated date must be after the created date.")
                .When(request => request.UpdatedAt.HasValue);
        }
    }
}
