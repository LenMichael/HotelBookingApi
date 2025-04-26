using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class HotelValidator : AbstractValidator<Hotel>
    {
        public HotelValidator()
        {
            RuleFor(hotel => hotel.Name)
                .NotEmpty().WithMessage("Hotel name is required.")
                .MaximumLength(100).WithMessage("Hotel name cannot be longer than 100 characters.");

            RuleFor(hotel => hotel.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot be longer than 200 characters.");

            RuleFor(hotel => hotel.PhoneNumber)
                .MaximumLength(15).WithMessage("Phone number cannot be longer than 15 characters.")
                .When(hotel => !string.IsNullOrEmpty(hotel.PhoneNumber));

            RuleFor(hotel => hotel.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(hotel => !string.IsNullOrEmpty(hotel.Email));
        }
    }
}
