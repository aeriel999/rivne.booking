namespace Rivne.Booking.Application.Authentication.Register;

public class RegisterUserCommandHandler(IUserRepository userRepository,
    IEmailConfirmationTokenService emailConfirmationTokenService) :
    IRequestHandler<RegisterUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        //ToDo ???? maybe make method isExist etc....
        var isUserExist = await userRepository.FindByEmilAsync(request.Email);

        if (isUserExist.FirstError.Type.HasFlag(ErrorType.Unexpected))
            return Error.Unexpected(isUserExist.FirstError.Description);

        if (!isUserExist.IsError)
            return Error.Validation("User with such email is already exist");

        var createUserResult = await userRepository.CreateUserAsync(request.Email, request.Password, request.Role);

        if (createUserResult.IsError)
            return createUserResult;

        var errorOrConfirmEmail = await emailConfirmationTokenService
            .SendConfirmationEmailAsync(createUserResult.Value, request.BaseUrl);

        if (errorOrConfirmEmail.IsError)
            return errorOrConfirmEmail.Errors;

        return createUserResult;
    }
}