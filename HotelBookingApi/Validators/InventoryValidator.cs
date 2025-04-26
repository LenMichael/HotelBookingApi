using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validators
{
    public class InventoryValidator : AbstractValidator<Inventory>
    {
        public InventoryValidator()
        {
            RuleFor(inventory => inventory.Name)
                .NotEmpty().WithMessage("Item name is required.")
                .MaximumLength(100).WithMessage("Item name cannot be longer than 100 characters.");

            RuleFor(inventory => inventory.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a non-negative value.");

            RuleFor(inventory => inventory.LastUpdated)
                .NotEmpty().WithMessage("Last updated date is required.");
        }
    }
}
