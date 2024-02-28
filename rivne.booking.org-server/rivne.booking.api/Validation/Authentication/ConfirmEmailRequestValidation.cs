namespace rivne.booking.api.Validation.Authentication;

public class ConfirmEmailRequestValidation : AbstractValidator<ConfirmEmailRequest>
{
	public ConfirmEmailRequestValidation()
	{
		//ToDo ???Is it wright Validation
		RuleFor(r => r.UserId).NotEmpty().WithMessage("Field must not be empty")
			.MaximumLength(254).MinimumLength(24);

		RuleFor(r => r.Token).NotEmpty().WithMessage("Field must not be empty")
			.MaximumLength(4096).MinimumLength(256);
	}
}
