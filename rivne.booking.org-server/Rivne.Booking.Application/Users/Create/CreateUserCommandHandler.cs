using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.Create;

public class CreateUserCommandHandler(IUserRepository _userRepository, IMapper _mapper) :
    IRequestHandler<CreateUserCommand, ErrorOr<User>>
{

    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var errorOrCreated = await _userRepository.CreateUserAsync(_mapper.Map<User>(request), request.Password);

        return errorOrCreated;
    }
}
