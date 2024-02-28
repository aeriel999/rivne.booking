namespace Rivne.Booking.Application.Users.GetAllUsers;

public record GetAllUsersQuery() : IRequest<ErrorOr<List<User>>>;

