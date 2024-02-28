namespace Rivne.Booking.Application.Authentication.RefreshToken;

public class TokenRefreshCommandHandler(IJwtService jwtService) :
    IRequestHandler<TokenRefreshCommand, ErrorOr<UserTokens>>
{
    public async Task<ErrorOr<UserTokens>> Handle(TokenRefreshCommand request,
        CancellationToken cancellationToken)
    {
        var refreshTokenResult = await jwtService.VerifyTokenAsync(request.Token, request.RefreshToken);

        return refreshTokenResult;
    }
}
