using FluentValidation;
using rivne.booking.api.Contracts.User;

namespace rivne.booking.api.Validation.User;

public class DeleteUserRequestValidation : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserRequestValidation()
    {
		RuleFor(r => r.UserId).NotEmpty().WithMessage("Field must not be empty");
	}
}
