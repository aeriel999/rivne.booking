namespace Rivne.Booking.Application.Interfaces;

public interface IEmailConfirmationTokenService
{
	Task<ErrorOr<Success>> SendConfirmationEmailAsync(User user, string baseUrl);
	Task<ErrorOr<Success>> ConfirmEmailAsync(string userId, string token);
	Task<ErrorOr<Success>> ForgotPasswordAsync(User user);
}
