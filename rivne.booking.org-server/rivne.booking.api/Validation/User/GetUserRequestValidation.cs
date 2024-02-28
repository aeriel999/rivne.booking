using FluentValidation;
using rivne.booking.api.Contracts.User.GetUser;

namespace rivne.booking.api.Validation.User;

public class GetUserRequestValidation : AbstractValidator<GetUserRequest>
{
    public GetUserRequestValidation()
    {
		RuleFor(r => r.UserId).NotEmpty().WithMessage("Field must not be empty");
	}
}
