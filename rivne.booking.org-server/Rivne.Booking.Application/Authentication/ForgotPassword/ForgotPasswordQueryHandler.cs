using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;
using System.Text;

namespace Rivne.Booking.Application.Authentication.ForgotPassword;

public class ForgotPasswordQueryHandler(IUserRepository userRepository) : 
	IRequestHandler<ForgotPasswordQuery, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
	{
		var isUserExist = await userRepository.FindByEmilAsync(request.Email);

		if (isUserExist.IsError)
			return Error.Failure(isUserExist.FirstError.Description);

		
	}
}
