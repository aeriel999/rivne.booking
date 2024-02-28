using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Users.SetAvatar;

public record AddAvatarCommand(string UserId, IFormFile Avatar) : IRequest<ErrorOr<User>>;
