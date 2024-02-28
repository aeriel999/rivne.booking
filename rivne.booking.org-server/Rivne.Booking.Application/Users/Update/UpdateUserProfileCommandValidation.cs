using FluentValidation;

namespace Rivne.Booking.Application.Users.Update;

public class UpdateUserProfileCommandValidation : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidation()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");

        When(r => !string.IsNullOrEmpty(r.FirstName), () =>
        {
            RuleFor(r => r.FirstName)
                .MinimumLength(3).WithMessage("Name must have at least 3 symbols");
        });

        When(r => !string.IsNullOrEmpty(r.LastName), () =>
        {
            RuleFor(r => r.LastName)
                .MinimumLength(3).WithMessage("Lastname must have at least 3 symbols");
        });

        When(r => !string.IsNullOrEmpty(r.PhoneNumber), () =>
        {
            RuleFor(r => r.PhoneNumber)
                .MinimumLength(11).WithMessage("Phone number must have at least 11 symbols");
        });
    }
}
