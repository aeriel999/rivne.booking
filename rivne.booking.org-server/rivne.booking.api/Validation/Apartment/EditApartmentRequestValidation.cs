using FluentValidation;
using rivne.booking.api.Contracts.Apartment;

namespace rivne.booking.api.Validation.Apartment;

public class EditApartmentRequestValidation : AbstractValidator<EditApartmentRequest>
{
    public EditApartmentRequestValidation()
    {
		//ToDo Bool Not empty?
		RuleFor(r => r.NumberOfBuilding).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.IsPrivateHouse).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.NumberOfRooms).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.Area).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.Price).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.Description).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.TypeOfBooking).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.StreetName).NotEmpty().WithMessage("Field must not be empty");
		When(r => r.Floor.HasValue, () =>
		{
			RuleFor(r => r.Floor)
				 .GreaterThan(0).WithMessage("Floor must be greater than 0");
		});
	}
}
