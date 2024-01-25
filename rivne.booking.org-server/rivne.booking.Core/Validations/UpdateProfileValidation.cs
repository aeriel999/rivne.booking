using FluentValidation;
using rivne.booking.Core.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Validations;
public class UpdateProfileValidation :AbstractValidator<UpdateProfileDto>
{
    public UpdateProfileValidation()
    {
		RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
		
		When(r => !string.IsNullOrEmpty(r.FirstName), () =>
		{
			RuleFor(r => r.FirstName)
				.MinimumLength(3).WithMessage("Name must have at least 3 symbols");
		});

		When(r => !string.IsNullOrEmpty(r.LastName), () =>
		{
			RuleFor(r => r.LastName)
				.MinimumLength(3).WithMessage("Lastname must have at least 3 symbols");
		});

		When(r => !string.IsNullOrEmpty(r.PhoneNumber), () =>
		{
			RuleFor(r => r.PhoneNumber)
				.MinimumLength(11).WithMessage("Phone number must have at least 11 symbols");
		});

	}
}
