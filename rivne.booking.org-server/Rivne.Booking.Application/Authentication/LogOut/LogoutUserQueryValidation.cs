namespace Rivne.Booking.Application.Authentication.LogOut;

public class LogoutUserQueryValidation : AbstractValidator<LogoutUserQuery>
{
    public LogoutUserQueryValidation()
    {
        RuleFor(r => r.UserId).NotEmpty().MinimumLength(24).MaximumLength(256);
    }
}
