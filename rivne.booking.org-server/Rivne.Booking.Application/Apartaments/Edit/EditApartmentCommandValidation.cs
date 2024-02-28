using FluentValidation;

namespace Rivne.Booking.Application.Apartaments.Edit;
public class EditApartmentCommandValidation : AbstractValidator<EditApartmentCommand>
{
    public EditApartmentCommandValidation()
    {
        RuleFor(r => r.NumberOfBuilding).NotEmpty().WithMessage("Field must not be empty")
            .GreaterThan(0).WithMessage("Number Of Building must be greater than 0");
        //sRuleFor(r => r.IsPrivateHouse).NotEmpty().WithMessage("Field must not be empty");
        RuleFor(r => r.NumberOfRooms).NotEmpty().WithMessage("Field must not be empty")
            .GreaterThan(0).WithMessage("Number Of Rooms must be greater than 0");
        RuleFor(r => r.Area).NotEmpty().WithMessage("Field must not be empty")
            .GreaterThan(0).WithMessage("Area must be greater than 0");
        RuleFor(r => r.Price).NotEmpty().WithMessage("Field must not be empty")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(r => r.Description).NotEmpty().WithMessage("Field must not be empty")
            .MinimumLength(150).WithMessage("Description must ha at least 150 symbols")
            .MaximumLength(1000).WithMessage("Description must be less than 1000 symbols");
        RuleFor(r => r.TypeOfBooking).NotEmpty().WithMessage("Field must not be empty")
            .MinimumLength(9).WithMessage("Type Of Booking must be at least 5 symbols")
            .MaximumLength(1000).WithMessage("Type Of Booking must be less than 25 symbols");
        RuleFor(r => r.StreetName).NotEmpty().WithMessage("Field must not be empty")
            .MinimumLength(9).WithMessage("Type Of Booking must be at least 5 symbols")
            .MaximumLength(1000).WithMessage("Type Of Booking must be less than 25 symbols");
        When(r => r.Floor.HasValue, () =>
        {
            RuleFor(r => r.Floor)
                 .GreaterThan(0).WithMessage("Floor must be greater than 0");
        });
    }
}
