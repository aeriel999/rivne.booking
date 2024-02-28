namespace Rivne.Booking.Application.Authentication.Confirmation;

public record ConfirmEmailQuery(
    string UserId,
    string Token) : IRequest<ErrorOr<Success>>;

