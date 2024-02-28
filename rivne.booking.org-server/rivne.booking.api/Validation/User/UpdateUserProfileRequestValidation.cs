using FluentValidation;
using rivne.booking.api.Contracts.User;

namespace rivne.booking.api.Validation.User;

public class UpdateUserProfileRequestValidation : AbstractValidator<UpdateUserProfileRequest>
{
    public UpdateUserProfileRequestValidation()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
    }
}
