namespace rivne.booking.api.Validation.Authentication;

public class TokenRefreshRequestValidation : AbstractValidator<TokenRefreshRequest>
{
    public TokenRefreshRequestValidation()
    {
        RuleFor(r => r.Token).NotEmpty()
			.MaximumLength(4096);

        RuleFor(r => r.RefreshToken).NotEmpty()
			.MaximumLength(4096);
    }
}
