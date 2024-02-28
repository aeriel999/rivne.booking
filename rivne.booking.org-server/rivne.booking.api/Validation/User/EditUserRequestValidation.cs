using FluentValidation;
using rivne.booking.api.Contracts.User;

namespace rivne.booking.api.Validation.User;

public class EditUserRequestValidation : AbstractValidator<EditUserRequest>
{
    public EditUserRequestValidation()
    {
        RuleFor(r => r.Role).NotEmpty().WithMessage("Field must not be empty");

        RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
    }
}
