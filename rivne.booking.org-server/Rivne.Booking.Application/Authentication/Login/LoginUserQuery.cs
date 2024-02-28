namespace Rivne.Booking.Application.Authentication.Login;

public record LoginUserQuery(
    string Email,
    string Password) : IRequest<ErrorOr<UserTokens>>;

