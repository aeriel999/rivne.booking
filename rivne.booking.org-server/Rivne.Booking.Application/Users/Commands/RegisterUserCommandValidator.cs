using FluentValidation;


namespace Rivne.Booking.Application.Users.Commands;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
        RuleFor(r => r.Password).NotEmpty().WithMessage("Field must not be empty").MinimumLength(6).WithMessage("Password must be at least 6 characters");
        RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Required field must not be empty.").MinimumLength(6).Equal(r => r.Password).WithMessage("Passwords are not matched");

    }
}
