using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.Update;

public record UpdateUserProfileCommand(
    string UserId,
    string? FirstName,
    string? LastName,
    string Email,
    string? PhoneNumber) : IRequest<ErrorOr<User>>;
