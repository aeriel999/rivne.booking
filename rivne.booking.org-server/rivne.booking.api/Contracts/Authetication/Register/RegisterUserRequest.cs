namespace rivne.booking.api.Contracts.Authetication.Register;

public record RegisterUserRequest(
    string Email,
    string Password,
    string ConfirmPassword,
    string Role);
