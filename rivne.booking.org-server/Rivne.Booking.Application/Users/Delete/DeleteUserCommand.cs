using ErrorOr;
using MediatR;

namespace Rivne.Booking.Application.Users.Delete;

public record DeleteUserCommand(string UserId) : IRequest<ErrorOr<Deleted>>;

