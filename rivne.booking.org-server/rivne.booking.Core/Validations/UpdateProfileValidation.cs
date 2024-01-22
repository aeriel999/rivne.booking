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
		RuleFor(r => r.FirstName).MinimumLength(3).WithMessage("Name must have at least 3 symbols");
		RuleFor(r => r.LastName).MinimumLength(3).WithMessage("Lastname must have at least 3 symbols");
		RuleFor(r => r.PhoneNumber).MinimumLength(11).WithMessage("Phone number have has at least 11 symbols");

	}
}
