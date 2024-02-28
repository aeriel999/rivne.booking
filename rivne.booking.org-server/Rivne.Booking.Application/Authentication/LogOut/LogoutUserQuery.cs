namespace Rivne.Booking.Application.Authentication.LogOut;

public record LogoutUserQuery(string UserId) : IRequest<ErrorOr<Success>>;
