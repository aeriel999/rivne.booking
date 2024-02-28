namespace Rivne.Booking.Application.Authentication.Register;

public record RegisterUserCommand(
   string Email,
   string Password,
   string ConfirmPassword,
   string Role,
   string BaseUrl) : IRequest<ErrorOr<User>>;
