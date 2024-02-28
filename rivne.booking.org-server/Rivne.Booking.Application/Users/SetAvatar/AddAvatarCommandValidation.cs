using FluentValidation;

namespace Rivne.Booking.Application.Users.SetAvatar;

public class AddAvatarCommandValidation : AbstractValidator<AddAvatarCommand>
{
    public AddAvatarCommandValidation()
    {
        //ToDo ??? Validation where read about it
        RuleFor(r => r.Avatar).NotEmpty().WithMessage("Field must not be empty");
        RuleFor(r => r.UserId).NotEmpty().WithMessage("Field must not be empty");
    }
}
