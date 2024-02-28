namespace rivne.booking.Infrastructure.Repository;

public class UserRepository(UserManager<AppUser> userManager) : IUserRepository
{
	public async Task<ErrorOr<User>> FindByEmilAsync(string email)
	{
		try
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
				return Error.NotFound("User is not found");

			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();

			if (role == null)
				return Error.NotFound("Role of user is not found");

			return AppUser.ToDomainUser(user, role);
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<User>> CreateAppUserAsync(AppUser appUser, string password, string role)
	{
		var errorOrCreated = await userManager.CreateAsync(appUser, password);

		if (!errorOrCreated.Succeeded)
			return Error.Failure("Error in creating of user");

		var errorOrAddToRole = await userManager.AddToRoleAsync(appUser, role);

		if (!errorOrAddToRole.Succeeded)
			return Error.Failure("Error in creating of user");

		return AppUser.ToDomainUser(appUser, role);
	}

	public async Task<ErrorOr<User>> CreateUserAsync(User user, string password)
	{
		try
		{
			var appUser =  AppUser.ToAppUser(user);

			var createAppUserResult = await CreateAppUserAsync(appUser, password, user.Role);

			return createAppUserResult;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<User>> CreateUserAsync(string email, string password, string role)
	{
		try
		{
			var appUser = new AppUser 
			{
				Email = email,
				UserName = email
			};

			var createAppUserResult = await CreateAppUserAsync(appUser, password, role);

			return createAppUserResult;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<List<User>>> GetAllUsersAsync()
	{
		try
		{
			//ToDo User + roles find 
			var users = await userManager.Users.ToListAsync();

			if (users == null)
			{
				return Error.NotFound("Users aren`t loaded");
			}

			var userList = await AppUser.ToDomainUsersListAsync(users, userManager);

			if (userList == null)
				return Error.NotFound("User aren`t loaded");

			return userList;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<User>> UpdateProfileAsync(User model)
	{
		try
		{
			var user = await userManager.FindByIdAsync(model.Id);

			if (user == null)
				return Error.NotFound("User is not found");

			if (user.FirstName != model.FirstName)
			{
				user.FirstName = model.FirstName;
			}

			if (user.LastName != model.LastName)
			{
				user.LastName = model.LastName;
			}

			//ToDo UserManager<TUser>.ChangeEmailAsync(TUser, String, String) Method
			//GenerateChangeEmailTokenAsync(TUser, String)
			//GenerateEmailConfirmationTokenAsync(TUser)
			//GetChangeEmailTokenPurpose(String)
			//GeneratePasswordResetTokenAsync(TUser)
			//GetAccessFailedCountAsync(TUser)
			if (user.Email != model.Email)
			{
				user.Email = model.Email;
				user.EmailConfirmed = false;
			}

			if (user.PhoneNumber != model.PhoneNumber)
			{
				user.PhoneNumber = model.PhoneNumber;
				user.PhoneNumberConfirmed = false;
			}

			var updatedResult = await userManager.UpdateAsync(user);
			if (!updatedResult.Succeeded)
				return Error.Failure("User is not update");

			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault()!;
			if (role == null)
				return Error.Failure("Error in updating");

			return AppUser.ToDomainUser(user, role);
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteUserAsync(string id)
	{
		try
		{
			var user = await userManager.FindByIdAsync(id);

			if (user == null)
				return Error.NotFound("User is not found");

			//ToDo Make method in ImageStorageService
			
			if (!user.Avatar.IsNullOrEmpty())
			{
				//ToDo Redirect Path to configuration
				var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "avatars");

				var delFilePath = Path.Combine(uploadFolderPath, user.Avatar!);

				if (File.Exists(delFilePath)) { File.Delete(delFilePath); }
			}

			var deleteUserResult = await userManager.DeleteAsync(user);

			if (!deleteUserResult.Succeeded)
				return Error.Failure("User is not delete");

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<User>> GetUserAsync(string userID)
	{
		try
		{
			var user = await userManager.FindByIdAsync(userID);

			if (user == null)
				return Error.NotFound("User is not found");

			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault()!;
			if (role == null)
				return Error.Failure("User role is not found");

			var domainUser = AppUser.ToDomainUser(user, role);

			return domainUser;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<User>> EditUserAsync(User model)
	{
		//ToDo Edit and Update has common code
		try
		{
			var user = await userManager.FindByIdAsync(model.Id);

			if (user == null)
				return Error.NotFound("User is not found");

			if (user.FirstName != model.FirstName)
			{
				user.FirstName = model.FirstName;
			}

			if (user.LastName != model.LastName)
			{
				user.LastName = model.LastName;
			}

			if (user.Email != model.Email)
			{
				user.Email = model.Email;
				user.EmailConfirmed = false;
			}

			if (user.EmailConfirmed != model.IsEmailConfirmed)
			{
				user.EmailConfirmed = model.IsEmailConfirmed;
			}

			if (user.PhoneNumber != model.PhoneNumber)
			{
				user.PhoneNumber = model.PhoneNumber;
				user.PhoneNumberConfirmed = false;
			}

			//ToDo Admin can it update?

			if (user.PhoneNumberConfirmed != model.PhoneNumberConfirmed)
			{
				user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
			}

			if (user.LockoutEnabled != model.LockoutEnabled)
			{
				user.LockoutEnabled = model.LockoutEnabled;
			}

			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();

			if (role == null)
				return Error.NotFound("Error in finding of role of user");

			if (role != model.Role)
			{
				await userManager.RemoveFromRoleAsync(user, role);

				await userManager.AddToRoleAsync(user, model.Role);

				role = model.Role;
			}

			var errorOrEdit = await userManager.UpdateAsync(user);

			if (!errorOrEdit.Succeeded)
				return Error.Failure("User is not update");

			return AppUser.ToDomainUser(user, role);
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<User>> FindByIdAsync(string userId)
	{
		var user = await userManager.FindByIdAsync(userId);

		if (user == null)
			return Error.NotFound("User is not found");

		var role = (await userManager.GetRolesAsync(user)).FirstOrDefault()!;

		if (role == null)
			return Error.NotFound("Role of user is not found");

		return AppUser.ToDomainUser(user, role);
	}
}
