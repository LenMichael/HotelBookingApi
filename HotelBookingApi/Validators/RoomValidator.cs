using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(room => room.RoomNumber)
                .GreaterThan(0).WithMessage("Room number must be greater than 0.");

            RuleFor(room => room.Type)
                .NotEmpty().WithMessage("Room type is required.")
                .MaximumLength(50).WithMessage("Room type cannot be longer than 50 characters.");

            RuleFor(room => room.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive value.");

            RuleFor(room => room.HotelId)
                .GreaterThan(0).WithMessage("Hotel ID must be greater than 0.");
        }
    }
}
