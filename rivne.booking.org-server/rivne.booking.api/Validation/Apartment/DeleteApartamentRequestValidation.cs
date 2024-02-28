using FluentValidation;
using rivne.booking.api.Contracts.Apartment;

namespace rivne.booking.api.Validation.Apartment;

public class DeleteApartamentRequestValidation : AbstractValidator<DeleteApartamentRequest>
{
    public DeleteApartamentRequestValidation()
    {
        RuleFor(r => r.Id).NotEmpty().WithMessage("Field must not be empty");
	}
}
