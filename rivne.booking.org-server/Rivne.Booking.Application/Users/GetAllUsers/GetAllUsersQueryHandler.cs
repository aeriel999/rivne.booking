namespace Rivne.Booking.Application.Users.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository) :
    IRequestHandler<GetAllUsersQuery, ErrorOr<List<User>>>
{
    public async Task<ErrorOr<List<User>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var errorOrAllUsers = await userRepository.GetAllUsersAsync();

        return errorOrAllUsers;
    }
}
