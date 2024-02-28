namespace rivne.booking.api.Validation.Authentication;

public class ForgotPasswordRequestValidation : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidation()
    {
		RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty")
			.EmailAddress().WithMessage("Wrong email format")
			.MaximumLength(254).MinimumLength(8);
	}
}
