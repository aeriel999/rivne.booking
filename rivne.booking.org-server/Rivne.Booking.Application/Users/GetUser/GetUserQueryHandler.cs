using ErrorOr;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository) :
    IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var erroeOrUser = await userRepository.GetUserAsync(request.userId);

        return erroeOrUser;
    }
}
