using Microsoft.AspNetCore.Identity;
using Rivne.Booking.Domain.Apartments;
using Rivne.Booking.Domain.Users;

namespace rivne.booking.Infrastructure.Common.Identity;

public class AppUser : IdentityUser
{
	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public string? Avatar { get; set; }

	public ICollection<IdentityRole> Roles { get; set; } = [];

	public static AppUser ToAppUser(User user)
	{
		var appUser =  new AppUser 
		{
			Email = user.Email,
			EmailConfirmed = user.IsEmailConfirmed,
			FirstName = user.FirstName,
			LastName = user.LastName,
			PhoneNumber = user.PhoneNumber,
			PhoneNumberConfirmed = user.PhoneNumberConfirmed,
			LockoutEnabled = user.LockoutEnabled,
			Avatar = user.Avatar,
		//	Apartments = user.Apartments,
			UserName = user.Email
		};

		return appUser;
	}

	public static User ToDomainUser(AppUser user, string role)
	{
		var domainUser = new User
		{
			Id = user.Id,
			Email = user.Email!,
			IsEmailConfirmed = user.EmailConfirmed,
			FirstName = user.FirstName,
			LastName = user.LastName,
			PhoneNumber = user.PhoneNumber,
			PhoneNumberConfirmed = user.PhoneNumberConfirmed,
			LockoutEnabled = user.LockoutEnabled,
			Avatar = user.Avatar,
		//	Apartments = user.Apartments,
			Role = role
		};

		return domainUser;
	}


	//ToDo Change it
	public static async Task<List<User>> ToDomainUsersListAsync(List<AppUser> appUsers,
		UserManager<AppUser> userManager)
	{
		var domainUserList = new List<User>();

		foreach (var user in appUsers)
		{
			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault()!;

			domainUserList.Add(AppUser.ToDomainUser(user, role));
		}

		return domainUserList;
	}
}
