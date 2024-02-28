using FluentValidation;
using rivne.booking.api.Contracts.User;

namespace rivne.booking.api.Validation.User;

public class AddAvatarRequestValidation : AbstractValidator<AddAvatarRequest>
{
    public AddAvatarRequestValidation()
    {
		RuleFor(r => r.File).NotEmpty().WithMessage("Field must not be empty");

	}
}
