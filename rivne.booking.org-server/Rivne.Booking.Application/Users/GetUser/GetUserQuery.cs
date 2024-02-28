using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.GetUser;

public record GetUserQuery(string userId) : IRequest<ErrorOr<User>>;
