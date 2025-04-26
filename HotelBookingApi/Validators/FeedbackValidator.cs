using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class FeedbackValidator : AbstractValidator<Feedback>
    {
        public FeedbackValidator()
        {
            RuleFor(feedback => feedback.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

            RuleFor(feedback => feedback.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(500).WithMessage("Message cannot be longer than 500 characters.");

            RuleFor(feedback => feedback.CreatedAt)
                .NotEmpty().WithMessage("Created date is required.");
        }
    }
}
