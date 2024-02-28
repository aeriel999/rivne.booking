namespace Rivne.Booking.Application.Interfaces;

public interface IUserRepository 
{
	Task<ErrorOr<User>> FindByEmilAsync(string email);
	Task<ErrorOr<User>> FindByIdAsync(string userId);
	Task<ErrorOr<User>> CreateUserAsync(User user, string password);
	Task<ErrorOr<User>> CreateUserAsync(string email, string password, string role);
	Task<ErrorOr<List<User>>> GetAllUsersAsync();
	Task<ErrorOr<User>> UpdateProfileAsync(User user);
	Task<ErrorOr<Deleted>> DeleteUserAsync(string userId);
	Task<ErrorOr<User>> GetUserAsync(string userId);
	Task<ErrorOr<User>> EditUserAsync(User user);
}
