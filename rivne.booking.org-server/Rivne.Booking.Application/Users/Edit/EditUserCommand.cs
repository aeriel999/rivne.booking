using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.Edit;
public record EditUserCommand(
    string Id,
    string? FirstName,
    string? LastName,
    string Email,
    bool EmailConfirmed,
    string? PhoneNumber,
    bool PhoneNumberConfirmed,
    bool LockoutEnabled,
    string Role) : IRequest<ErrorOr<User>>;
