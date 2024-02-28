using ErrorOr;
using Microsoft.AspNetCore.Http;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Interfaces;

public interface IImageStorageService
{
	Task<ErrorOr<User>> AddAvatarAsync(User user, IFormFile file);
	Task<ErrorOr<string>> SaveImageAsync(IFormFile image);

	Task<ErrorOr<Deleted>> DeleteImageAsync(string imageName);
}
