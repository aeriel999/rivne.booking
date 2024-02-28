﻿namespace rivne.booking.api.Validation.Authentication;

public class LoginUserRequestValidation : AbstractValidator<LoginUserRequest>
{
	public LoginUserRequestValidation()
	{
		RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty")
			.EmailAddress().WithMessage("Wrong email format")
			.MaximumLength(254).MinimumLength(8);

		RuleFor(r => r.Password).NotEmpty().WithMessage("Field must not be empty")
			.MaximumLength(24).MinimumLength(8);
	}
}