using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Core.Automapper;
using rivne.booking.Core.Services;
 

namespace rivne.booking.Core;
public static class ServiceExtensions
{
	public static void AddCoreServices(this IServiceCollection services)
	{
		services.AddTransient<UserService>();
		services.AddTransient<JwtServise>();
	}

	public static void AddMappings(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(AutoMapperUserProfile));
	}
}
