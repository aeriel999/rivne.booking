namespace Rivne.Booking.Application.Interfaces;

public interface IAppAuthenticationService
{
	Task<ErrorOr<UserTokens>> LoginUserAsync(string email, string password);
	Task<ErrorOr<Success>> LogoutUserAsync(string userId);
}
