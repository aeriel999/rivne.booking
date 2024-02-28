namespace rivne.booking.Infrastructure.Services;

public class EmailConfirmationTokenService(IEmailService emailService,
	UserManager<AppUser> userManager) : IEmailConfirmationTokenService
{
	public async Task<ErrorOr<Success>> SendConfirmationEmailAsync(User user, string baseUrl)
	{
		try
		{
			var newUser = await userManager.FindByEmailAsync(user.Email);

			if (newUser == null)
				return Error.NotFound(user.Email);

			var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);

			var encodedEmailToken = Encoding.UTF8.GetBytes(token);

			var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

			string url = $"http://{baseUrl}/api/User/ConfirmEmail?userid={newUser.Id}&token={validEmailToken}";

			string emailBody = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now</a>";

			await emailService.SendEmailAsync(newUser.Email!, "Email confirmation.", emailBody);

			return Result.Success;
		}
		catch (Exception ex)
		{
			return Error.Failure(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Success>> ConfirmEmailAsync(string userId, string token)
	{
		var user = await userManager.FindByIdAsync(userId);

		if (user == null)
			return Error.NotFound(userId);

		var decoderToken = WebEncoders.Base64UrlDecode(token);

		var normalToken = Encoding.UTF8.GetString(decoderToken);

		var confirmEmailResult = await userManager.ConfirmEmailAsync(user, normalToken);

		if (!confirmEmailResult.Succeeded)
			return Error.Failure("User email is not confirmed!");

		return Result.Success;
	}


	//ToDo ??? Is ForgotPassword in this servise
	public async Task<ErrorOr<Success>> ForgotPasswordAsync(User user)
	{
		var token = await _userManager.GeneratePasswordResetTokenAsync(user);
		var encodedToken = Encoding.UTF8.GetBytes(token);
		var validToken = WebEncoders.Base64UrlEncode(encodedToken);

		//string url = $"{_configuration["HostSettings:URL"]}/ResetPassword?email={email}&token={validToken}";
		//string emailBody = "<h1>Follow the instructions to reset your password</h1>" + $"<p>To reset your password <a href='{url}'>Click here</a></p>";
		//await _emailService.SendEmailAsync(email, "Fogot password", emailBody);

		//return new ServiceResponse
		//{
		//	Success = true,
		//	Message = $"Reset password for {_configuration["HostSettings:URL"]} has been sent to the email successfully!"
		//};
	}
}
