namespace Rivne.Booking.Application.Authentication.RefreshToken;

public class TokenRefreshCommandValidation : AbstractValidator<TokenRefreshCommand>
{
    public TokenRefreshCommandValidation()
    {
		RuleFor(r => r.Token).NotEmpty()
		   .MaximumLength(4096);

		RuleFor(r => r.RefreshToken).NotEmpty()
			.MaximumLength(4096);
	}
}
