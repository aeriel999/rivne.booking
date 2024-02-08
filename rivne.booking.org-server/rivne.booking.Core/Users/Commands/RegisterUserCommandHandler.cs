namespace rivne.booking.Core.Users.Commands;
using ErrorOr;
using Rivne.Booking.Domain.Users;

public class RegisterUserCommandHandler
{
	public async Task<ErrorOr<User>> Execute(RegisterUserCommand command)
	{
		var validator = new RegisterUserCommandValidator();

		var validationResult = validator.Validate(command);

		if(!validationResult.IsValid) 
		{ 
			
		}

		return new User();
	}
}
