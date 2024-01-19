﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Core.Interfaces;
using rivne.booking.Infrastructure.Context;
using rivne.booking.Infrastructure.Repository;
 

namespace rivne.booking.Infrastructure;
public static class ServiceExtensions
{
	public static void AddDbContext(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<ApiDbContext>(opt =>
		{
			opt.UseSqlServer(connectionString);
			opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});
	}

	public static void AddInfrastructureService(this IServiceCollection services)
	{
		services.AddIdentity<User, IdentityRole>(option =>
		{
			option.SignIn.RequireConfirmedEmail = true;
			option.Lockout.MaxFailedAccessAttempts = 5;
			option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			option.Password.RequireDigit = true;
			option.Password.RequireLowercase = true;
			option.Password.RequireUppercase = true;
			option.Password.RequiredLength = 6;
			option.Password.RequireNonAlphanumeric = true;
			option.User.RequireUniqueEmail = true;
		})
			.AddEntityFrameworkStores<ApiDbContext>().AddDefaultTokenProviders();
	}

	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
	}
}
