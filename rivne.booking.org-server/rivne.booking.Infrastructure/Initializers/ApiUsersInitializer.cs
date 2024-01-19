using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Infrastructure.Context;


namespace rivne.booking.Infrastructure.Initializers;
public static class ApiUsersInitializer
{
	public static void SeedUsers(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().HasData(new User[]
			{
				new User
				{
					FirstName = "Bob",
					UserName = "admin@email.com",
					Email = "admin@email.com",
					NormalizedEmail = "admin@email.com".ToUpperInvariant(),
					EmailConfirmed = true,
					PhoneNumber = "+xx(xxx)xxx-xx-xx",
					PhoneNumberConfirmed = true,
					 
				},
				new User
				{
					FirstName = "Alice",
					 UserName = "user1@email.com",
					Email = "user1@email.com",
					NormalizedEmail = "user1@email.com".ToUpperInvariant(),
					EmailConfirmed = true,
					PhoneNumber = "+xx(xxx)xxx-xx-xx",
					PhoneNumberConfirmed = true
				}
		});
	}

	public static void SeedRoles(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]
			{
				 new IdentityRole()
					{
						Name = "Administrator",
						NormalizedName = "ADMINISTRATOR",
					},
					new IdentityRole()
					{
						Name = "User",
						NormalizedName = "USER"
					}
			});
	}

	public static async Task SeedPasswordsAndRoles(IApplicationBuilder applicationBuilder)
	{
		using (var serviseScope = applicationBuilder.ApplicationServices.CreateScope())
		{
			var context = serviseScope.ServiceProvider.GetService<ApiDbContext>();

			UserManager<User> userManager = serviseScope.ServiceProvider.GetRequiredService<UserManager<User>>();

			var admin = await userManager.FindByEmailAsync("admin@email.com");
			var user = await userManager.FindByEmailAsync("user1@email.com");

			IdentityResult adminResult = userManager.AddPasswordAsync(admin, "Admin+1111").Result;

			IdentityResult user1Result = userManager.AddPasswordAsync(user, "User+1111").Result;

				if (adminResult.Succeeded)
				{
					userManager.AddToRoleAsync(admin, "Administrator").Wait();
				}
				if (user1Result.Succeeded)
				{
					userManager.AddToRoleAsync(user, "User").Wait();
				}
			 
		}
	}
}
