using ErrorOr;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.SetAvatar;

public class AddAvatarCommandHandler(IUserRepository userRepository,
    IImageStorageService imageStorageService) :
    IRequestHandler<AddAvatarCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await userRepository.FindByIdAsync(request.UserId);

        if (isUserExist.IsError)
            return isUserExist;

        var errorOrUser = await imageStorageService.AddAvatarAsync(isUserExist.Value, request.Avatar);

        return errorOrUser;
    }
}
