using FluentValidation;
using rivne.booking.api.Contracts;

namespace rivne.booking.api.Validation;

public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidation()
    {
		RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
		RuleFor(r => r.Password).NotEmpty().WithMessage("Field must not be empty");
		RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Required field must not be empty.").Equal(r => r.Password).WithMessage("Passwords are not matched");

	}
}
