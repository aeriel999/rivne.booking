namespace Rivne.Booking.Application.Authentication.LogOut;

public class LogoutUserQueryHandler(IAppAuthenticationService authenticationService) :
    IRequestHandler<LogoutUserQuery, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
    {
        var logoutResult = await authenticationService.LogoutUserAsync(request.UserId);

        return logoutResult;
    }
}

