namespace Rivne.Booking.Application.Users.Commands;
using ErrorOr;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

public class RegisterUserCommandHandler(IUserRepository userRepository)
{
    public async Task<ErrorOr<User>> Execute(RegisterUserCommand command)
    {
        var validator = new RegisterUserCommandValidator();

		var validationResult = validator.Validate(command);

		if (!validationResult.IsValid)
		{
			return new ErrorOr<User>().Value;
		}

		var result = await userRepository.CreateAsync(command.Email, command.Password);

		return result;

		//if (result.Succeeded)
		//{
		//	var roleResult = _userManager.AddToRoleAsync(user, "User").Result;

		//	await SendConfirmationEmailAsync(user);

		//	var tokens = await _jwtService.GenerateJwtTokensAsync(user);

		//	if (roleResult.Succeeded)
		//	{
		//		return new ServiceResponse
		//		{
		//			Success = true,
		//			Message = "User is created"
		//		};
		//	}
		//	else
		//	{
		//		return new ServiceResponse
		//		{
		//			Success = false,
		//			Message = "User is not created",
		//		};
		//	}
		//}
		//else
		//{
		//	return new ServiceResponse
		//	{
		//		Success = false,
		//		Errors = result.Errors.Select(e => e.Description)
		//	};
		//}
	}
}
