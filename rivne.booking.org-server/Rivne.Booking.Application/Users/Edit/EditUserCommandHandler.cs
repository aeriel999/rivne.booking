using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.Edit;

public class EditUserCommandHandler(IUserRepository _userRepository, IMapper _mapper) :
    IRequestHandler<EditUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var errorOrSuccess = await _userRepository.EditUserAsync(_mapper.Map<User>(request));

        return errorOrSuccess;
    }
}
