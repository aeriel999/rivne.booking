namespace rivne.booking.api.Validation.User;

public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidation()
    {
        RuleFor(r => r.Email).NotEmpty().WithMessage("Field must not be empty").EmailAddress().WithMessage("Wrong email format");
        RuleFor(r => r.Role).NotEmpty().WithMessage("Field must not be empty");
        RuleFor(r => r.Password).NotEmpty().WithMessage("Field must not be empty");
        RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Required field must not be empty.");
    }
}