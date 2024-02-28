namespace rivne.booking.Infrastructure.Services;

public class AppAuthenticationService(SignInManager<AppUser> signInManager,
	UserManager<AppUser> userManager, IJwtService jwtService) : IAppAuthenticationService
{
	public async Task<ErrorOr<UserTokens>> LoginUserAsync(string email, string password)
	{
		try
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
				return Error.NotFound(email);

			var signinResult = await signInManager.PasswordSignInAsync(user, password,
				isPersistent: true, lockoutOnFailure: true);

			if (!signinResult.Succeeded)
				return Error.Unexpected();

			if (signinResult.IsNotAllowed)
				return Error.Validation("Email is not confirmed");

			if (signinResult.IsLockedOut)
				return Error.Validation("User is blocked");

			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();

			if (role == null)
				return Error.NotFound("Role of user is not found");

			var domenUser = AppUser.ToDomainUser(user, role);

			var tokens = await jwtService.GenerateJwtTokensAsync(domenUser);

			return tokens;
		}
		catch (Exception ex)
		{
			return Error.Failure(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Success>> LogoutUserAsync(string userId)
	{
		try
		{
			var tokens = await jwtService.GetTokensByUserId(userId);

			foreach (RefreshToken token in tokens.Value)
			{
				await jwtService.Delete(token);
			}

			await signInManager.SignOutAsync();

			return Result.Success;

		}
		catch (Exception ex)
		{
			return Error.Failure(ex.Message.ToString());
		}
	}
}
