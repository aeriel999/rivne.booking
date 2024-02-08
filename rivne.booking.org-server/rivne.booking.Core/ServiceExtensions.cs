using Microsoft.Extensions.DependencyInjection;
using rivne.booking.Core.Mapper;
using rivne.booking.Core.Services;


namespace rivne.booking.Core;
public static class ServiceExtensions
{
	public static void AddCoreServices(this IServiceCollection services)
	{
		services.AddTransient<UserService>();
		//services.AddTransient<JwtServise>(); 
		services.AddTransient<ApartmentService>();
		//services.AddTransient<EmailService>();

	}

	public static void AddMappings(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(AutoMapperUserProfile));
		services.AddAutoMapper(typeof(AutoMapperApartmentProfile));

	}
}
