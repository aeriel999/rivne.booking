using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Infrastructure.Context;
using System.Data;
using System;


namespace rivne.booking.Infrastructure.Initializers;
public static class ApiUsersInitializer
{
	public static class Roles
	{
		public static List<string> All = new()
		{
			Admin,
			User
		};
		public const string Admin = "Admin";
		public const string User = "User";
	}
	public static void SeedData(this IApplicationBuilder app)
	{
		using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			var service = scope.ServiceProvider;
			//Отримую посилання на наш контекст
			var context = service.GetRequiredService<ApiDbContext>();

			context.Database.Migrate();

			var userManager = scope.ServiceProvider
				.GetRequiredService<UserManager<User>>();

			var roleManager = scope.ServiceProvider
				.GetRequiredService<RoleManager<IdentityRole>>();


			#region Add users and roles

			if (!context.Roles.Any())
			{
				foreach (var role in Roles.All)
				{
					var result = roleManager.CreateAsync(new IdentityRole
					{
						Name = role
					}).Result;
				}
			}

			if (!context.Users.Any())
			{
				var user = new User
				{
					FirstName = "Jone",
					LastName = "Dou",
					Email = "admin@email.com",
					UserName = "admin@email.com",
					EmailConfirmed = true,
				};
				var result = userManager.CreateAsync(user, "Admin+1111").Result;
				if (result.Succeeded)
				{
					result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
				}
			}

			#endregion
		}
	}
}
