using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(eventModel => eventModel.Name)
                .NotEmpty().WithMessage("Event name is required.")
                .MaximumLength(100).WithMessage("Event name cannot be longer than 100 characters.");

            RuleFor(eventModel => eventModel.Date)
                .NotEmpty().WithMessage("Event date is required.");

            RuleFor(eventModel => eventModel.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(100).WithMessage("Location cannot be longer than 100 characters.");

            RuleFor(eventModel => eventModel.Organizer)
                .MaximumLength(200).WithMessage("Organizer name cannot be longer than 200 characters.")
                .When(eventModel => !string.IsNullOrEmpty(eventModel.Organizer));

            RuleFor(eventModel => eventModel.Description)
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.")
                .When(eventModel => !string.IsNullOrEmpty(eventModel.Description));
        }
    }
}
