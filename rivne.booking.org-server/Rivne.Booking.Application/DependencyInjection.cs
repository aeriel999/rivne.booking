namespace Rivne.Booking.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		//ToDo ???? New Extention
		//services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
		services.AddMappings();

		services.AddMediatR(typeof(DependencyInjection).Assembly);
		
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			 
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		return services;
	}
}
