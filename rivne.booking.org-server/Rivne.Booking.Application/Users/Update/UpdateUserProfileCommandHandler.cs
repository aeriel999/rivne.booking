using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.Update;

public class UpdateUserProfileCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<UpdateUserProfileCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(UpdateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        var errorOrSuccess = await userRepository.UpdateProfileAsync(mapper.Map<User>(request));

        return errorOrSuccess;
    }
}
