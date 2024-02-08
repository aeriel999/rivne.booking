using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Infrastructure.Context;
using rivne.booking.Core.Entities.Apartments;
using Rivne.Booking.Domain.Users;


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
			if (!context.Streets.Any())
			{
				var street = new Street
				{
					Id = 1,
					Name = "Soborna",
				};
				context.Streets.Add(street);
				context.SaveChanges();
			}

			if (!context.Apartments.Any())
			{
				var apartment = new Apartment
				{
					Id = 1,
					UserId = "ab4176bd-4aee-4bad-9e39-5b71bdc1d2a3",
					StreetId = 1,
					 NumberOfBuilding = 1,
					  IsPrivateHouse = false,
					 NumberOfRooms = 1,
					 Floor = 1,
					  Area = 25,
					 Price = 8000,
					 Description = "Nice",
					TypeOfBooking = "For mounth",
					DateOfPost = DateTime.Now.ToUniversalTime(),
					IsArchived = false,
					IsPosted = false,

				};

				context.Apartments.Add(apartment);
				context.SaveChanges();
			}
		}
	}
}
