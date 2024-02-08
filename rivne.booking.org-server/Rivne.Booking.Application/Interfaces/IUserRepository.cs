
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Interfaces;
public interface IUserRepository 
{
	Task<ErrorOr<User>> CreateAsync(string email, string password);
}
