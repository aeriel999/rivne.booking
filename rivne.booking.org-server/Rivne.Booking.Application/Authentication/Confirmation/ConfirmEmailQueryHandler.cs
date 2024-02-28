namespace Rivne.Booking.Application.Authentication.Confirmation;

public class ConfirmEmailQueryHandler(IEmailConfirmationTokenService emailConfirmationTokenService) :
    IRequestHandler<ConfirmEmailQuery, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(
        ConfirmEmailQuery request, CancellationToken cancellationToken)
    {
        var errorOrSuccess = await emailConfirmationTokenService.ConfirmEmailAsync(request.UserId,
            request.Token);

        return errorOrSuccess;
    }
}
