namespace Rivne.Booking.Application.Authentication.ForgotPassword;

public record ForgotPasswordQuery(string Email) : IRequest<ErrorOr<Success>>;
 
