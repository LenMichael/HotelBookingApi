using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(payment => payment.BookingId)
                .GreaterThan(0).WithMessage("Booking ID must be greater than 0.");

            RuleFor(payment => payment.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("Amount must be a positive value.");

            RuleFor(payment => payment.PaymentDate)
                .NotEmpty().WithMessage("Payment date is required.");

            RuleFor(payment => payment.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .MaximumLength(50).WithMessage("Payment method cannot be longer than 50 characters.");
        }
    }
}
