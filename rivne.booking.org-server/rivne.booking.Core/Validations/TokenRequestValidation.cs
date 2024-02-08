using FluentValidation;
using rivne.booking.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Validations;
public class TokenRequestValidation : AbstractValidator<TokenRequestDto>
{
    public TokenRequestValidation()
    {
		RuleFor(r => r.Token).NotEmpty();
		RuleFor(r => r.RefreshToken).NotEmpty();
	}
}
