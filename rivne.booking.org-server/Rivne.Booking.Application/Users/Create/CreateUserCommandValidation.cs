using FluentValidation;

namespace Rivne.Booking.Application.Users.Create;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");

        RuleFor(r => r.Role).NotEmpty().WithMessage("Field must not be empty");

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

        RuleFor(r => r.Password).NotEmpty().WithMessage("Field must not be empty").MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Required field must not be empty.").MinimumLength(6).Equal(r => r.Password).WithMessage("Passwords are not matched");
    }

}
