using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.Create;

public record CreateUserCommand(
    string? FirstName,
    string? LastName,
    string Email,
    string? PhoneNumber,
    string Role,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<User>>;
