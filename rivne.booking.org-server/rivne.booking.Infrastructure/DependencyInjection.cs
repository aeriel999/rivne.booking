using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Infrastructure.Common.Identity;
using rivne.booking.Infrastructure.Common.Persistence;
using Rivne.Booking.Application.Interfaces;
using rivne.booking.Infrastructure.Repository;
using rivne.booking.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;

namespace rivne.booking.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		services
			.AddPersistence(configuration)
			.AddAppIdentity()
			.AddRepositories()
			.AddInfrastructureServices();

		return services;
	}

	private static IServiceCollection AddPersistence(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		string connStr = configuration.GetConnectionString("DefaultConnection")!;

		services.AddDbContext<ApiDbContext>(opt =>
		{
			//opt.UseSqlServer(connectionString);
			opt.UseNpgsql(connStr);

			opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});

		return services;
	}

	private static IServiceCollection AddAppIdentity(this IServiceCollection services)
	{
		services.AddIdentity<AppUser, IdentityRole>(option =>
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

		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IApartmentsRepository, ApartmentsRepository>();
		services.AddScoped<IImageRepository, ImageRepository>();
		services.AddScoped<IStreetRepository, StreetRepository>();
		services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

		return services;
	}

	private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddScoped<IEmailService, EmailService>();
		services.AddScoped<IJwtService, JwtService>();
		services.AddScoped<IAppAuthenticationService, AppAuthenticationService>();
		services.AddScoped<IEmailConfirmationTokenService, EmailConfirmationTokenService>();
		services.AddScoped<IImageStorageService, ImageStorageService>();

		services.AddTransient<EmailService>();
		services.AddTransient<JwtService>();
		services.AddTransient<AppAuthenticationService>();
		services.AddTransient<EmailConfirmationTokenService>();
		services.AddTransient<ImageStorageService>();


		return services;
	}
}
