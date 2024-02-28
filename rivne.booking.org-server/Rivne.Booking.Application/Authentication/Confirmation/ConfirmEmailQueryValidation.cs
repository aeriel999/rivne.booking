namespace Rivne.Booking.Application.Authentication.Confirmation;

public class ConfirmEmailQueryValidation : AbstractValidator<ConfirmEmailQuery>
{
    public ConfirmEmailQueryValidation()
    {
		RuleFor(r => r.UserId).NotEmpty().WithMessage("Field must not be empty")
		   .MaximumLength(254).MinimumLength(24);

		RuleFor(r => r.Token).NotEmpty().WithMessage("Field must not be empty")
			.MaximumLength(4096).MinimumLength(256);
	}
}
