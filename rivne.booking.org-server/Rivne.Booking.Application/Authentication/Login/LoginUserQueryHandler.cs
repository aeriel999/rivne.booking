namespace Rivne.Booking.Application.Authentication.Login;

public class LoginUserQueryHandler(IAppAuthenticationService authenticationService) :
    IRequestHandler<LoginUserQuery, ErrorOr<UserTokens>>
{
    public async Task<ErrorOr<UserTokens>> Handle(LoginUserQuery command, CancellationToken cancellationToken)
    {
        var loginResult = await authenticationService.LoginUserAsync(command.Email, command.Password);

        return loginResult;
    }
}
