using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(booking => booking.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(booking => booking.RoomId)
                .GreaterThan(0).WithMessage("Room ID must be greater than 0.");

            RuleFor(booking => booking.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(booking => booking.CheckInDate)
                .NotEmpty().WithMessage("Check-in date is required.")
                .LessThan(booking => booking.CheckOutDate)
                .WithMessage("Check-in date must be before the check-out date.");

            //RuleFor(booking => booking.CheckInDate)
            //    .NotEmpty().WithMessage("Check-in date is required.");

            RuleFor(booking => booking.CheckOutDate)
                .NotEmpty().WithMessage("Check-out date is required.");

            RuleFor(booking => booking.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(50).WithMessage("Status cannot be longer than 50 characters.");

            //RuleFor(booking => booking)
            //    .Must(booking => booking.CheckOutDate > booking.CheckInDate.AddDays(1))
            //    .WithMessage("Check-out date must be at least 1 day after check-in date.");
        }
    }
}
