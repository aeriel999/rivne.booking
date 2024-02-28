namespace Rivne.Booking.Application.Authentication.RefreshToken;

public record TokenRefreshCommand(string Token, string RefreshToken) :
    IRequest<ErrorOr<UserTokens>>;

