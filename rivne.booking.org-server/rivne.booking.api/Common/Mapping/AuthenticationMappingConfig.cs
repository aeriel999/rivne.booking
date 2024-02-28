namespace rivne.booking.api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<(RegisterUserRequest registerRequest, string BaseUrl), RegisterUserCommand>()
		.Map(dest => dest.BaseUrl, src => src.BaseUrl)
		.Map(dest => dest, src => src.registerRequest);

		config.NewConfig<LoginUserRequest, LoginUserQuery>();

		config.NewConfig<UserTokens, LoginUserResponse>()
			.Map(dest => dest.Token, src => src.Token)
			.Map(dest => dest.RefreshToken, src => src.RefreshToken.Token);

		config.NewConfig<TokenRefreshRequest, TokenRefreshCommand>();

		config.NewConfig<ForgotPasswordQuery, ForgotPasswordRequest>();

	}
}
