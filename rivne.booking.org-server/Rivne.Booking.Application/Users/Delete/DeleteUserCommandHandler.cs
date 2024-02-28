using ErrorOr;
using MediatR;
using Rivne.Booking.Application.Interfaces;

namespace Rivne.Booking.Application.Users.Delete;

public class DeleteUserCommandHandler(IUserRepository userRepository) :
    IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var errorOrDelete = await userRepository.DeleteUserAsync(request.UserId);

        return errorOrDelete;
    }
}
