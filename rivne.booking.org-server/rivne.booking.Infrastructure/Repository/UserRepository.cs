using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using rivne.booking.Core.Services;
using rivne.booking.Infrastructure.Services;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;
using System.Text;

namespace rivne.booking.Infrastructure.Repository;

public class UserRepository(
	UserManager<User> _userManager, 
	IConfiguration _config,
	EmailService _emailService,
	JwtServise _jwtService) : IUserRepository
{
	public async Task<ErrorOr<User>> CreateAsync(string email, string password)
	{
		var user = new User
		{
			Email = email,
			UserName = email,
		};

		var createUserResult = await _userManager.CreateAsync(user, password);

		if (!createUserResult.Succeeded) 
		{
			return Error.Failure("Error in creating of user");
		}

		var addToRoleResult = _userManager.AddToRoleAsync(user, "User").Result;

		if (!addToRoleResult.Succeeded) 
		{
			return Error.Failure("Error in creating of user");
		}

		await SendConfirmationEmailAsync(user);

		var tokens = await _jwtService.GenerateJwtTokensAsync(user);

		return user;
	}

	public async Task<ErrorOr<Success>> SendConfirmationEmailAsync(User newUser)
	{
		try
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

			var encodedEmailToken = Encoding.UTF8.GetBytes(token);
			var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

			string url = $"{_config["HostSettings:URL"]}/api/User/confirmemail?userid={newUser.Id}&token={validEmailToken}";

			string emailBody = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now</a>";
			await _emailService.SendEmailAsync(newUser.Email!, "Email confirmation.", emailBody);

			return Result.Success;
		}
		catch (Exception ex)
		{
			return Error.Failure(ex.Message.ToString());
		}
	}
}
