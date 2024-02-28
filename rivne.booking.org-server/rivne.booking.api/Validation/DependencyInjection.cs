namespace rivne.booking.api.Validation;

public static class DependencyInjection
{
	public static IServiceCollection AddRequestValidation(this IServiceCollection services)
	{
		//ToDo make sure it is nessesary code
		services.AddScoped<IValidator<ConfirmEmailRequest>, ConfirmEmailRequestValidation>();
		services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidation>();
		services.AddScoped<IValidator<EditUserRequest>, EditUserRequestValidation>();
		services.AddScoped<IValidator<LoginUserRequest>, LoginUserRequestValidation>();
		services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidation>();
		services.AddScoped<IValidator<TokenRefreshRequest>, TokenRefreshRequestValidation>();
		services.AddScoped<IValidator<UpdateUserProfileRequest>, UpdateUserProfileRequestValidation>();
		services.AddScoped<IValidator<CreateApartmentRequest>, CreateApartmentRequestValidation>();
		services.AddScoped<IValidator<EditApartmentRequest>, EditApartmentRequestValidation>();

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

		return services;
	}
}
